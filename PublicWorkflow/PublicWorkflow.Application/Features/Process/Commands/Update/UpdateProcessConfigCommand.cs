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
        public bool? IsDeleted { get; set; }

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
                    ProcessConfig.IsDeleted = command.IsDeleted ?? ProcessConfig.IsDeleted;
                    await _ProcessConfigRepository.UpdateAsync(ProcessConfig);

                    return Result<long>.Success(ProcessConfig.Id);
                }
            }
        }
    }
}