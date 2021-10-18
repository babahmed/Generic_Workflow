using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System.Linq;
using PublicWorkflow.Application.Interfaces.Shared;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class CreateProcessConfigCommand : IRequest<Result<long>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovalLevels { get; set; }
        public Publish PublishType { get; set; }
        public string FeedBackUrl { get; set; }
        public bool SingleRejection { get; set; }
        public bool NotifyAllApproverOnApproval { get; set; }
        public bool NotifyInitiatorOnApproval { get; set; }
        public bool AttachApprovalPdf { get; set; }
        public bool IncludeApproverDetails { get; set; }
        public bool RequiresAllLevelsForRejection { get; set; }
    }

    public class CreateProcessConfigCommandHandler : IRequestHandler<CreateProcessConfigCommand, Result<long>>
    {
        private readonly IGenericRepository<ProcessConfig> _ProcessConfigRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _user;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateProcessConfigCommandHandler(
            IGenericRepository<ProcessConfig> ProcessConfigRepository,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticatedUserService _user,
            IMapper mapper)
        {
            _ProcessConfigRepository = ProcessConfigRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            this._user = _user;
        }

        public async Task<Result<long>> Handle(CreateProcessConfigCommand request, CancellationToken cancellationToken)
        {
            var config = _mapper.Map<ProcessConfig>(request);
            config.OrganizationId= _user.OId;
            await _ProcessConfigRepository.AddAsync(config);

            return Result<long>.Success(config.Id);
        }
    }
}