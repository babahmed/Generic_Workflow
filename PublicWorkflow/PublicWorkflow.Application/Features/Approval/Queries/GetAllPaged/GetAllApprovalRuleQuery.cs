
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
    public class GetAllApprovalRuleQuery : IRequest<PaginatedResult<GetAllApprovalRuleResponse>>
    {
        public string Search { get; set; }
        public long ApprovalConfigId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllApprovalRuleQuery(string search, long approvalConfigId, int? pageNumber = 1, int? pageSize = 20)
        {
            Search = search;
            ApprovalConfigId = approvalConfigId;
            PageNumber = pageNumber.Value;
            PageSize = pageSize.Value;
        }
    }

    public class GetAllApprovalRuleQueryHandler : IRequestHandler<GetAllApprovalRuleQuery, PaginatedResult<GetAllApprovalRuleResponse>>
    {
        private readonly IGenericRepository<ApprovalRule> _approvalRuleepository;
        private readonly IMapper _mapper;

        public GetAllApprovalRuleQueryHandler(IGenericRepository<ApprovalRule> _approvalRuleepository, IMapper mapper)
        {
            this._approvalRuleepository = _approvalRuleepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllApprovalRuleResponse>> Handle(GetAllApprovalRuleQuery request, CancellationToken cancellationToken)
        {
            var dataQuery = await _approvalRuleepository.GetAllAsync(c =>
            c.ApprovalConfigId==request.ApprovalConfigId &&
            (string.IsNullOrEmpty(request.Search)
            || c.Name.ToUpper().Contains(request.Search.ToUpper())
            ));
            var record = await dataQuery.OrderByDescending(x => x.Id).ToMappedPaginatedListAsync<ApprovalRule,GetAllApprovalRuleResponse>(request.PageNumber, request.PageSize,_mapper);

            record.Message = record.TotalCount > 0 ? "data retrieved ok" : "No data found";

            return record;
        }
    }
}