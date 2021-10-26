using PublicWorkflow.Application.Interfaces.Repositories;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Domain.Entities.Catalog;

namespace PublicWorkflow.Application.Features.Commands.Update
{
    public class UpdateApprovalConfigCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? RequiredApprovers { get; set; }
        public string[] Approvers { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateApprovalConfigCommand, Result<long>>
        {
            private readonly IGenericRepository<ApprovalConfig> _ApprovalConfigRepository;

            public UpdateProductCommandHandler(IGenericRepository<ApprovalConfig> ApprovalConfigRepository)
            {
                _ApprovalConfigRepository = ApprovalConfigRepository;
            }

            public async Task<Result<long>> Handle(UpdateApprovalConfigCommand command, CancellationToken cancellationToken)
            {
                var ApprovalConfig = await _ApprovalConfigRepository.GetByIdAsync(command.Id);

                if (ApprovalConfig == null)
                {
                    return Result<long>.Fail($"Approval Config Not Found.");
                }
                else
                {
                    ApprovalConfig.Name = command.Name ?? ApprovalConfig.Name;
                    ApprovalConfig.Description = command.Description ?? ApprovalConfig.Description;
                    ApprovalConfig.RequiredApprovers = command.RequiredApprovers ?? ApprovalConfig.RequiredApprovers;
                    ApprovalConfig.Approvers = command.Approvers ?? ApprovalConfig.Approvers;

                    await _ApprovalConfigRepository.UpdateAsync(ApprovalConfig);

                    return Result<long>.Success(ApprovalConfig.Id);
                }
            }
        }
    }
}