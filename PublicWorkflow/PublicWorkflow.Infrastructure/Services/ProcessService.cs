using PublicWorkflow.Application.Constants;
using PublicWorkflow.Application.Features.Commands.Update;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Application.Interfaces.Service;
using PublicWorkflow.Application.Interfaces.Shared;
using PublicWorkflow.Domain.Entities.Catalog;
using PublicWorkflow.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Infrastructure.Services
{
    public class ProcessService : IProcessService
    {
        private readonly IGenericRepository<ApprovalConfig> _ApprovalConfigRepository;
        private readonly IGenericRepository<ProcessConfig> _ProcessConfigRepository;
        private readonly IGenericRepository<Process> _ProcessRepository;
        private readonly IGenericRepository<Approval> _ApprovalRepository;
        private readonly IGenericRepository<ProcessRule> _ProcessRuleRepository;
        private readonly IGenericRepository<ApprovalRule> _ApprovalRuleRepository;
        private readonly IGenericRepository<History> _historyRepository;
        private readonly IDateTimeService _date;
        private readonly IPublishService _publisher;

            public ProcessService(
                IGenericRepository<ApprovalConfig> _ApprovalConfigRepository,
                IGenericRepository<ProcessConfig> _ProcessConfigRepository,
            IGenericRepository<Process> _ProcessRepository,
            IGenericRepository<Approval> _ApprovalRepository,
            IGenericRepository<ProcessRule> _ProcessRuleRepository,
            IGenericRepository<ApprovalRule> _ApprovalRuleRepository,
            IGenericRepository<History> _historyRepository,
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
                this._date = _date;
                this._historyRepository = _historyRepository;
                this._publisher = _publisher;
            }


            public async Task PostApproval(UpdateLevelCommand request, IAuthenticatedUserService user)
        {
            bool Processcomplete;
            var logs = new List<History>();

            var Approval = await _ApprovalRepository.GetByIdAsync(request.ApprovalId);
            var ApprovalConfig = await _ApprovalConfigRepository.GetByIdAsync(Approval.ApprovalconfigId);
            var process = await _ProcessRepository.GetAsync(c => c.Id == Approval.ProcessId);
            //Load the level rules
            var approvalRules = await _ApprovalRuleRepository.GetAllAsync(c => c.ApprovalConfigId == ApprovalConfig.Id);
            var processRules = await _ProcessRuleRepository.GetAllAsync(c => c.ProcessConfigId == process.Id);

            //Apply Apporoval level rules
            //if no rule is set....Apply default
            if (!approvalRules.Any())
            {
                //if request is to reject
                if (request.Status == Status.Rejected)
                {
                    //Update Approvals
                    Approval.Status = Status.Rejected;
                    Approval.Treated = true;
                    Approval.Actioned = _date.NowUtc;

                }

                //Has everyone approved?
                if (ApprovalConfig.Approvers.Length == Approval.AlreadyApproved.Length && request.Status == Status.Approved)
                {
                    Approval.Actioned = _date.NowUtc;
                    Approval.Treated = true;
                }
            }
            else
            {

            }


            //Apply Process level rule
            if (!processRules.Any())
            {
                //If no rule set on process, apply default
                if (!processRules.Any())
                {
                    //Update process
                    process.Status = Status.Rejected;
                    process.Completed = _date.NowUtc;
                    Processcomplete = true;

                    logs.Add(new History()
                    {
                        Action = Messages.ProcessJob.CompletedRejected,
                        ProcessId = process.Id,
                        Username = user.UserName,
                        ApprovalId = Approval.Id
                    });
                }
            }
            else
            {

            }
            //Check if Job Level is complete

        }
    }
}
