
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Application.Features.Queries.GetById;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;

namespace PublicWorkflow.Application.Features.Queries.GetById
{
    public class GetProcessConfigByIdQuery : IRequest<Result<GetProcessConfigByIdResponse>>
    {
        public long Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProcessConfigByIdQuery, Result<GetProcessConfigByIdResponse>>
        {
            private readonly IGenericRepository<ProcessConfig> _ProcessConfigCache;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IGenericRepository<ProcessConfig> productCache, IMapper mapper)
            {
                _ProcessConfigCache = productCache;
                _mapper = mapper;
            }

            public async Task<Result<GetProcessConfigByIdResponse>> Handle(GetProcessConfigByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _ProcessConfigCache.GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetProcessConfigByIdResponse>(product);
                return Result<GetProcessConfigByIdResponse>.Success(mappedProduct);
            }
        }
    }
}