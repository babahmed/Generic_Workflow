using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Domain.Enum;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class CreateApprovalConfigCommand : IRequest<Result<long>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ProcessConfigId { get; set; }
        public int Level { get; set; }
        public int RequiredApprovers { get; set; }
        public string[] Approvers { get; set; }
    }

    public class CreateApprovalConfigCommandHandler : IRequestHandler<CreateApprovalConfigCommand, Result<long>>
    {
        private readonly IGenericRepository<ApprovalConfig> _ApprovalConfigRepository;
        private readonly IMapper _mapper;

        public CreateApprovalConfigCommandHandler(IGenericRepository<ApprovalConfig> ApprovalConfigRepository, IMapper mapper)
        {
            _ApprovalConfigRepository = ApprovalConfigRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(CreateApprovalConfigCommand request, CancellationToken cancellationToken)
        {
            var config = _mapper.Map<ApprovalConfig>(request);
            await _ApprovalConfigRepository.AddAsync(config);

            return Result<long>.Success(config.Id);
        }
    }
}