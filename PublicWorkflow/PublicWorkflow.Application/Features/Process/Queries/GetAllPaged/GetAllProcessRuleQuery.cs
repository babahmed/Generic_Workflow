
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
    public class GetAllProcessRuleQuery : IRequest<PaginatedResult<GetAllProcessRuleResponse>>
    {
        public string Search { get; set; }
        public long ProcessConfigId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllProcessRuleQuery(string search, long processConfigId, int? pageNumber = 1, int? pageSize = 20)
        {
            Search = search;
            ProcessConfigId = processConfigId;
            PageNumber = pageNumber.Value;
            PageSize = pageSize.Value;
        }
    }

    public class GetAllProcessRuleQueryHandler : IRequestHandler<GetAllProcessRuleQuery, PaginatedResult<GetAllProcessRuleResponse>>
    {
        private readonly IGenericRepository<ProcessRule> _ruleRepository;
        private readonly IMapper _mapper;

        public GetAllProcessRuleQueryHandler(
            IGenericRepository<ProcessRule> _ruleRepository, 
            IMapper mapper)
        {
            this._ruleRepository = _ruleRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllProcessRuleResponse>> Handle(GetAllProcessRuleQuery request, CancellationToken cancellationToken)
        {

            var dataQuery = await _ruleRepository.GetAllAsync(c =>
            c.ProcessConfigId==request.ProcessConfigId &&
            (string.IsNullOrEmpty(request.Search)
            || c.Name.ToUpper().Contains(request.Search.ToUpper())
            ));

            var record = await dataQuery.OrderByDescending(x => x.Id).ToMappedPaginatedListAsync<ProcessRule,GetAllProcessRuleResponse>(request.PageNumber, request.PageSize,_mapper);


            record.Message = record.TotalCount > 0 ? "data retrieved ok" : "No data found";

            return record;
        }
    }
}