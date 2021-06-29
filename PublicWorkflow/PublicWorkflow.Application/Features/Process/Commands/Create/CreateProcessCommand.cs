using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class CreateProcessCommand : IRequest<Result<long>>
    {
        public long ProcessConfigId { set; get; }
        public string[] Attachements { set; get; }
        public string Data { set; get; }
    }

    public class CreateProcessCommandHandler : IRequestHandler<CreateProcessCommand, Result<long>>
    {
        private readonly IGenericRepository<ProcessConfig> _ProcessConfigRepository;
        private readonly IGenericRepository<Process> _ProcessRepository;
        private readonly IMediator _mediator;

        public CreateProcessCommandHandler(
            IGenericRepository<ProcessConfig> ProcessConfigRepository, 
            IGenericRepository<Process> ProcessRepository,
            IMediator mediator)
        {
            _ProcessConfigRepository = ProcessConfigRepository;
            _ProcessRepository = ProcessRepository;
            _mediator = mediator;
        }

        public async Task<Result<long>> Handle(CreateProcessCommand request, CancellationToken cancellationToken)
        {
            var config = await _ProcessConfigRepository.GetByIdAsync(request.ProcessConfigId);

            if(config==null || config.IsDeleted)
            {
                return Result<long>.Fail("Invalid configuration");
            }
            var process = new Process()
            {
                Attachements = request.Attachements,
                JobReferenceId = Guid.NewGuid().ToString().Replace("-", ""),
                ProcessConfigId = config.Id,
                Data = request.Data
            };

            await _ProcessRepository.AddAsync(process);

            var approvaCommand = new CreateApprovalCommand() { ProcessConfigId = config.Id, ProcessId = process.Id };

            var approvals = await _mediator.Send(approvaCommand);

            return Result<long>.Success(config.Id,$"process addedd to {config.Name} with {approvals.Data} approval level(s)");
        }
    }
}