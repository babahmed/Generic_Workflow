
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;

namespace PublicWorkflow.Application.Features.Queries.GetById
{
    public class GetApprovalConfigByIdQuery : IRequest<Result<GetApprovalConfigByIdResponse>>
    {
        public long Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetApprovalConfigByIdQuery, Result<GetApprovalConfigByIdResponse>>
        {
            private readonly IGenericRepository<ApprovalConfig> _ApprovalConfig;
            private readonly IMapper _mapper;

            public GetProductByIdQueryHandler(IGenericRepository<ApprovalConfig> approvalConfig, IMapper mapper)
            {
                _ApprovalConfig = approvalConfig;
                _mapper = mapper;
            }

            public async Task<Result<GetApprovalConfigByIdResponse>> Handle(GetApprovalConfigByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _ApprovalConfig.GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetApprovalConfigByIdResponse>(product);
                return Result<GetApprovalConfigByIdResponse>.Success(mappedProduct);
            }
        }
    }
}