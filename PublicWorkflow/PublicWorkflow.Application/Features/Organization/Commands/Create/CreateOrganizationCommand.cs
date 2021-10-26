using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class CreateOrganizationCommand : IRequest<Result<long>>
    {
        public string Name { get; set; }
        public string Motto { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string ContactEmail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string LandMark { get; set; }
        public string Phone { get; set; }
    }

    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, Result<long>>
    {
        private readonly IGenericRepository<Organization> _OrganizationRepository;
        private readonly IMapper _mapper;

        public CreateOrganizationCommandHandler(IGenericRepository<Organization> OrganizationRepository, IMapper mapper)
        {
            _OrganizationRepository = OrganizationRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var org = _mapper.Map<Organization>(request);
            await _OrganizationRepository.AddAsync(org);
            return Result<long>.Success(org.Id);
        }
    }
}