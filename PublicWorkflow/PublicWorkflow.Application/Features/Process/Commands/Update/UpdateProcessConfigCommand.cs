using PublicWorkflow.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Domain.Entities.Catalog;
using PublicWorkflow.Domain.Enum;

namespace PublicWorkflow.Application.Features.ProcessConfigs.Commands.Update
{
    public class UpdateProcessConfigCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? RequiredApprovalLevels { get; set; }
        public Publish? PublishType { get; set; }
        public string FeedBackUrl { get; set; }
        public bool? SingleRejection { get; set; }
        public bool? NotifyAllApproverOnApproval { get; set; }
        public bool? NotifyInitiatorOnApproval { get; set; }
        public bool? AttachApprovalPdf { get; set; }
        public bool? IncludeApproverDetails { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProcessConfigCommand, Result<long>>
        {
            private readonly IGenericRepository<ProcessConfig> _ProcessConfigRepository;

            public UpdateProductCommandHandler(IGenericRepository<ProcessConfig> ProcessConfigRepository)
            {
                _ProcessConfigRepository = ProcessConfigRepository;
            }

            public async Task<Result<long>> Handle(UpdateProcessConfigCommand command, CancellationToken cancellationToken)
            {
                var ProcessConfig = await _ProcessConfigRepository.GetByIdAsync(command.Id);

                if (ProcessConfig == null)
                {
                    return Result<long>.Fail($"ProcessConfig Not Found.");
                }
                else
                {
                    ProcessConfig.Name = command.Name ?? ProcessConfig.Name;
                    ProcessConfig.Description = command.Description ?? ProcessConfig.Description;
                    ProcessConfig.RequiredApprovalLevels = command.RequiredApprovalLevels ?? ProcessConfig.RequiredApprovalLevels;
                    ProcessConfig.PublishType = command.PublishType ?? ProcessConfig.PublishType;
                    ProcessConfig.FeedBackUrl = command.FeedBackUrl ?? ProcessConfig.FeedBackUrl;
                    ProcessConfig.SingleRejection = command.SingleRejection ?? ProcessConfig.SingleRejection;
                    ProcessConfig.NotifyAllApproverOnApproval = command.NotifyAllApproverOnApproval ?? ProcessConfig.NotifyAllApproverOnApproval;
                    ProcessConfig.NotifyInitiatorOnApproval = command.NotifyInitiatorOnApproval ?? ProcessConfig.NotifyInitiatorOnApproval;
                    ProcessConfig.AttachApprovalPdf = command.AttachApprovalPdf ?? ProcessConfig.AttachApprovalPdf;
                    ProcessConfig.IncludeApproverDetails = command.IncludeApproverDetails ?? ProcessConfig.IncludeApproverDetails;
                    await _ProcessConfigRepository.UpdateAsync(ProcessConfig);

                    return Result<long>.Success(ProcessConfig.Id);
                }
            }
        }
    }
}