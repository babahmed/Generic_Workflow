
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Extensions;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Queries.GetAllPaged
{
    public class GetAllApprovalConfigQuery : IRequest<PaginatedResult<GetAllApprovalConfigResponse>>
    {
        public string Search { get; set; }
        public long? Level { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllApprovalConfigQuery(string search, long? level, int? pageNumber = 1, int? pageSize = 20)
        {
            Search = search;
            Level = level;
            PageNumber = pageNumber.Value;
            PageSize = pageSize.Value;
        }
    }

    public class GetAllApprovalConfigQueryHandler : IRequestHandler<GetAllApprovalConfigQuery, PaginatedResult<GetAllApprovalConfigResponse>>
    {
        private readonly IGenericRepository<ApprovalConfig> _approvalConfigRepository;
        private readonly IMapper _mapper;

        public GetAllApprovalConfigQueryHandler(IGenericRepository<ApprovalConfig> approvalConfigRepository, IMapper mapper)
        {
            _approvalConfigRepository = approvalConfigRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllApprovalConfigResponse>> Handle(GetAllApprovalConfigQuery request, CancellationToken cancellationToken)
        {
            var dataQuery = await _approvalConfigRepository.GetAllAsync(c =>
            (string.IsNullOrEmpty(request.Search)
            || c.Description.ToUpper().Contains(request.Search.ToUpper())
            || c.Name.ToUpper().Contains(request.Search.ToUpper())
            )
            &&
            (request.Level == null || c.Level == request.Level)
            );
            var record = await dataQuery.OrderByDescending(x => x.Id).ToMappedPaginatedListAsync<ApprovalConfig,GetAllApprovalConfigResponse>(request.PageNumber, request.PageSize,_mapper);


            record.Message = record.TotalCount > 0 ? "data retrieved ok" : "No data found";

            return record;
        }
    }
}