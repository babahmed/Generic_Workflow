using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Domain.Enum;
using System.Linq;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class CreateApprovalCommand : IRequest<Result<long>>
    {
        public long ProcessConfigId { get; set; }
        public long ProcessId { get; set; }
    }

    public class CreateApprovalCommandHandler : IRequestHandler<CreateApprovalCommand, Result<long>>
    {
        private readonly IGenericRepository<ApprovalConfig> _approvalConfigRepository;
        private readonly IGenericRepository<Approval> _approvalRepository;
        private readonly IMapper _mapper;

        public CreateApprovalCommandHandler(IGenericRepository<ApprovalConfig> approvalConfigRepository, IGenericRepository<Approval> approvalRepository, IMapper mapper)
        {
            _approvalConfigRepository = approvalConfigRepository;
            _approvalRepository = approvalRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(CreateApprovalCommand request, CancellationToken cancellationToken)
        {
            var configs = (await _approvalConfigRepository.GetAllAsync(c => c.ProcessConfigId == request.ProcessConfigId)).ToList();

            foreach (var config in configs)
            {
                var approval = new Approval()
                {
                    Status=config.Level==0?Status.InProcess:Status.New,
                    AlreadyApproved = new string[] { },
                    Comments = new string[] { },
                    ProcessId = request.ProcessId,
                    ApprovalconfigId=config.Id
                };
               await _approvalRepository.AddAsync(approval);
            }
            return Result<long>.Success(configs.Count());
        }
    }
}