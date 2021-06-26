
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
    public class GetAllOrganizationQuery : IRequest<Result<List<GetAllOrganizationResponse>>>
    {
        public string Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllOrganizationQuery(string search, int? pageNumber = 1, int? pageSize = 20)
        {
            Search = search;
            PageNumber = pageNumber.Value;
            PageSize = pageSize.Value;
        }
    }

    public class GetAllOrganizationQueryHandler : IRequestHandler<GetAllOrganizationQuery, Result<List<GetAllOrganizationResponse>>>
    {
        private readonly IGenericRepository<Organization> _orgRepo;
        private readonly IMapper _mapper;

        public GetAllOrganizationQueryHandler(IGenericRepository<Organization> orgRepo, IMapper mapper)
        {
            _orgRepo = orgRepo;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllOrganizationResponse>>> Handle(GetAllOrganizationQuery request, CancellationToken cancellationToken)
        {
            var dataQuery = await _orgRepo.GetAllAsync(c =>
                                (string.IsNullOrEmpty(request.Search)
                                || c.Description.ToUpper().Contains(request.Search.ToUpper())
                                || c.ContactEmail.ToUpper().Contains(request.Search.ToUpper())
                                || c.Name.ToUpper().Contains(request.Search.ToUpper())
                                || c.Phone.ToUpper().Contains(request.Search.ToUpper())
                                ));
            var record = await dataQuery.OrderByDescending(x => x.Id).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            var mapped = _mapper.Map<List<GetAllOrganizationResponse>>(record);
            return Result<List<GetAllOrganizationResponse>>.Success(mapped);
        }
    }
}