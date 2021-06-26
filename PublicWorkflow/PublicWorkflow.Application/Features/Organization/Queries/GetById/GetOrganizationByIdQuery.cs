
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;

namespace PublicWorkflow.Application.Features.Queries.GetById
{
    public class GetOrganizationByIdQuery : IRequest<Result<GetOrganizationByIdResponse>>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, Result<GetOrganizationByIdResponse>>
        {
            private readonly IGenericRepository<Organization> _orgRepo;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IGenericRepository<Organization> orgRepo, IMapper mapper)
            {
                _orgRepo = orgRepo;
                _mapper = mapper;
            }

            public async Task<Result<GetOrganizationByIdResponse>> Handle(GetOrganizationByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _orgRepo.GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetOrganizationByIdResponse>(product);
                return Result<GetOrganizationByIdResponse>.Success(mappedProduct);
            }
        }
    }
}