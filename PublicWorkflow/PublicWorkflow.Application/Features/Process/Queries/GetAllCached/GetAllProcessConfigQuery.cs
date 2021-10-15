
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

namespace PublicWorkflow.Application.Features.Queries.GetAll
{
    public class GetAllProcessConfigQuery : IRequest<Result<List<GetAllProcessConfigResponse>>>
    {
        public string Search { get; set; }
        public long? OrganizationId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllProcessConfigQuery(string search, long? organizationId, int? pageNumber=1,int?pageSize=20)
        {
            Search = search;
            OrganizationId = organizationId;
            PageNumber = pageNumber.Value;
            PageSize = pageSize.Value;
        }
    }

    public class GetAllProcessConfigQueryHandler : IRequestHandler<GetAllProcessConfigQuery, Result<List<GetAllProcessConfigResponse>>>
    {
        private readonly IGenericRepository<ProcessConfig> _processConfig;
        private readonly IMapper _mapper;

        public GetAllProcessConfigQueryHandler(IGenericRepository<ProcessConfig> processConfig, IMapper mapper)
        {
            _processConfig = processConfig;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllProcessConfigResponse>>> Handle(GetAllProcessConfigQuery request, CancellationToken cancellationToken)
        {
            var dataQuery = await _processConfig.GetAllAsync(c=>
            (string.IsNullOrEmpty(request.Search)
            || c.Description.ToUpper().Contains(request.Search.ToUpper())
            || c.Name.ToUpper().Contains(request.Search.ToUpper())
            || c.Organization.Name.ToUpper().Contains(request.Search.ToUpper())
            )
            &&
            (request.OrganizationId == null || c.OrganizationId == request.OrganizationId)
            );
            var record= await dataQuery.OrderByDescending(x => x.Id).ToPaginatedListAsync(request.PageNumber, request.PageSize);


            record.Message = record.TotalCount > 0 ? "data retrieved ok" : "No data found";

            var mappedConfigs = _mapper.Map<List<GetAllProcessConfigResponse>>(record);
            return Result<List<GetAllProcessConfigResponse>>.Success(mappedConfigs);
        }
    }
}