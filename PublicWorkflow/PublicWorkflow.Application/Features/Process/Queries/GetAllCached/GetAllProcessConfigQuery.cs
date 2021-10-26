
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Extensions;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Application.Interfaces.Shared;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Queries.GetAll
{
    public class GetAllProcessConfigQuery : IRequest<Result<PaginatedResult<GetAllProcessConfigResponse>>>
    {
        public string Search { get; set; }
        public long? OrganizationId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllProcessConfigQuery(string search, long? organizationId, int? pageNumber = 1, int? pageSize = 20)
        {
            Search = search;
            OrganizationId = organizationId;
            PageNumber = pageNumber.Value;
            PageSize = pageSize.Value;
        }
    }

    public class GetAllProcessConfigQueryHandler : IRequestHandler<GetAllProcessConfigQuery, Result<PaginatedResult<GetAllProcessConfigResponse>>>
    {
        private readonly IGenericRepository<ProcessConfig> _processConfig;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _user;

        public GetAllProcessConfigQueryHandler(IGenericRepository<ProcessConfig> processConfig, IMapper mapper, IAuthenticatedUserService _user)
        {
            _processConfig = processConfig;
            _mapper = mapper;
            this._user = _user;
        }

        public async Task<Result<PaginatedResult<GetAllProcessConfigResponse>>> Handle(GetAllProcessConfigQuery request, CancellationToken cancellationToken)
        {

            var dataQuery = await _processConfig.GetAllAsync(c =>
            (string.IsNullOrEmpty(request.Search)
            || c.Description.ToUpper().Contains(request.Search.ToUpper())
            || c.Name.ToUpper().Contains(request.Search.ToUpper())
            )
            &&
            (request.OrganizationId == null || c.OrganizationId == request.OrganizationId)
            &&
            (_user.UId == null || c.UserId == _user.UId)
            );
            var record = await dataQuery.OrderByDescending(x => x.Id)
                .Select(c => new GetAllProcessConfigResponse()
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    Description = c.Description,
                    IsEnabled = c.IsEnabled,
                    Name = c.Name,
                    OrganizationId = c.OrganizationId,
                    RequiredApprovalLevels = c.RequiredApprovalLevels
                })
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);


            record.Message = record.TotalCount > 0 ? "data retrieved ok" : "No data found";

            return Result<PaginatedResult<GetAllProcessConfigResponse>>.Success(record);
        }
    }
}