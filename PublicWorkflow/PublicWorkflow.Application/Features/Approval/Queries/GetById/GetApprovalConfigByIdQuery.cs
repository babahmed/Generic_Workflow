
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Queries.GetById
{
    public class GetApprovalConfigByIdQuery : IRequest<Result<GetApprovalConfigByIdResponse>>
    {
        public long Id { get; set; }

        public class GetApprovalConfigByIdQueryHandler : IRequestHandler<GetApprovalConfigByIdQuery, Result<GetApprovalConfigByIdResponse>>
        {
            private readonly IGenericRepository<ApprovalConfig> _ApprovalConfig;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;

            public GetApprovalConfigByIdQueryHandler(
                IGenericRepository<ApprovalConfig> approvalConfig, 
                IMapper mapper,
                IMediator _mediator
                )
            {
                _ApprovalConfig = approvalConfig;
                _mapper = mapper;
                this._mediator = _mediator;
            }

            public async Task<Result<GetApprovalConfigByIdResponse>> Handle(GetApprovalConfigByIdQuery query, CancellationToken cancellationToken)
            {
                var approvalConfig = await _ApprovalConfig.GetByIdAsync(query.Id);
                var mappedProduct = _mapper.Map<GetApprovalConfigByIdResponse>(approvalConfig);

                return Result<GetApprovalConfigByIdResponse>.Success(mappedProduct);
            }
        }
    }
}