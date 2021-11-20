using AspNetCoreHero.Results;
using Hangfire;
using MediatR;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Application.Interfaces.Service;
using PublicWorkflow.Application.Interfaces.Shared;
using PublicWorkflow.Domain.Entities.Catalog;
using PublicWorkflow.Domain.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Commands.Update
{
    public class UpdateLevelCommand : IRequest<Result<long>>
    {
        public Status Status { get; set; }
        public string Remark { get; set; }
        public int ApprovalId { get; set; }

        public class UpdateLevelCommandHandler : IRequestHandler<UpdateLevelCommand, Result<long>>
        {
            private readonly IGenericRepository<ApprovalConfig> _ApprovalConfigRepository;
            private readonly IGenericRepository<ProcessConfig> _ProcessConfigRepository;
            private readonly IGenericRepository<Process> _ProcessRepository;
            private readonly IGenericRepository<Approval> _ApprovalRepository;
            private readonly IGenericRepository<ProcessRule> _ProcessRuleRepository;
            private readonly IGenericRepository<ApprovalRule> _ApprovalRuleRepository;
            private readonly IGenericRepository<History> _historyRepository;
            private readonly IProcessService _processService;
            private readonly IAuthenticatedUserService _User;
            private readonly IDateTimeService _date;
            private readonly IPublishService _publisher;

            public UpdateLevelCommandHandler(
                IGenericRepository<ApprovalConfig> _ApprovalConfigRepository,
                IGenericRepository<ProcessConfig> _ProcessConfigRepository,
            IGenericRepository<Process> _ProcessRepository,
            IGenericRepository<Approval> _ApprovalRepository,
            IGenericRepository<ProcessRule> _ProcessRuleRepository,
            IGenericRepository<ApprovalRule> _ApprovalRuleRepository,
            IGenericRepository<History> _historyRepository,
            IAuthenticatedUserService _User,
            IProcessService _processService,
            IDateTimeService _date,
            IPublishService _publisher
                )
            {
                this._ApprovalConfigRepository = _ApprovalConfigRepository;
                this._ProcessConfigRepository = _ProcessConfigRepository;
                this._ProcessRepository = _ProcessRepository;
                this._ApprovalRepository = _ApprovalRepository;
                this._ProcessRuleRepository = _ProcessRuleRepository;
                this._ApprovalRuleRepository = _ApprovalRuleRepository;
                this._processService = _processService;
                this._User = _User;
                this._date = _date;
                this._historyRepository = _historyRepository;
                this._publisher = _publisher;
            }

            public async Task<Result<long>> Handle(UpdateLevelCommand command, CancellationToken cancellationToken)
            {
                var logs = new List<History>();
                //Load the Approval
                var Approval = await _ApprovalRepository.GetByIdAsync(command.ApprovalId);

                //check if approval is found
                if (Approval == null)
                {
                    return Result<long>.Fail($"Approvals not found");
                }

                //check if level is treated
                if (Approval.Treated)
                {
                    return Result<long>.Fail($"Approvals at this level has been completed.");
                }

                //check if user already approved in level
                if (Approval.AlreadyApproved.Contains(_User.UserName))
                {
                    return Result<long>.Fail($"User Already Approved at this level.");
                }

                //Load the Approval configuration
                var ApprovalConfig = await _ApprovalConfigRepository.GetByIdAsync(Approval.ApprovalconfigId);

                if (ApprovalConfig == null)
                {
                    return Result<long>.Fail($"approval Configuration not found for this level.");
                }

                //all Checks done... now lets add user to approvers
                Approval.AlreadyApproved = Approval.AlreadyApproved.Concat(new string[1] { _User.UserName }).ToArray();
                Approval.Comments = Approval.Comments.Concat(new string[1] { command.Remark }).ToArray();

                //await _ApprovalRepository.UpdateAsync(Approval);

                //Load Process
                var process = await _ProcessRepository.GetAsync(c => c.Id == Approval.ProcessId);

                if (process == null)
                {
                    return Result<long>.Fail($"approval process not found.");
                }

                //Add the status log
                logs.Add(new History()
                {
                    Action = $"Status: {command.Status} Comment:{command.Remark}",
                    ProcessId = process.Id,
                    Username = _User.UserName,
                    ApprovalId = Approval.Id
                });

                //Queue the post approval process job

                _processService.PostApproval(command, _User);
                //Save all updates

                await _ProcessRepository.UpdateAsync(process);
                await _ApprovalRepository.UpdateAsync(Approval);

                //Add all logs
                await _historyRepository.AddRangeAsync(logs);

                BackgroundJob.Enqueue(() => _publisher.PublishProcess(process.Id));

                return Result<long>.Success(ApprovalConfig.Id);
            }
        }
    }
}