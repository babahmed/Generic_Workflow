
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Extensions;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Application.Interfaces.Shared;
using PublicWorkflow.Domain.Entities.Catalog;
using PublicWorkflow.Domain.Enum;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Queries.GetAllPaged
{
    public class GetAllProcessQuery : IRequest<Result<PaginatedResult<ProcessView>>>
    {
        public string Search { get; set; }
        public long? Level { get; set; }
        public Status? Status { get; set; }
        [JsonIgnore]
        public bool MyProcess { get; set; }
        public int? ProcessId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllProcessQuery(string search, long? level, bool myProcess, int? processId, Status status, int? pageNumber = 1, int? pageSize = 20)
        {
            ProcessId = processId;
            MyProcess = myProcess;
            Search = search;
            Level = level;
            PageNumber = pageNumber.Value;
            PageSize = pageSize.Value;
            Status = status;
        }
    }

    public class GetAllProcessQueryHandler : IRequestHandler<GetAllProcessQuery, Result<PaginatedResult<ProcessView>>>
    {
        private readonly IGenericRepository<ProcessView> _approvalsRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUserService _user;

        public GetAllProcessQueryHandler(IGenericRepository<ProcessView> _approvalsRepository, IMapper mapper, IAuthenticatedUserService _user)
        {
            this._approvalsRepository = _approvalsRepository;
            _mapper = mapper;
            this._user = _user;
        }

        public async Task<Result<PaginatedResult<ProcessView>>> Handle(GetAllProcessQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProcessView> dataQuery = null;

            if (request.MyProcess)
            {
                dataQuery = await _approvalsRepository.GetAllAsync(c =>
                 (string.IsNullOrEmpty(request.Search)
                 || c.LevelName.ToUpper().Contains(request.Search.ToUpper())
                 || c.LevelDescription.ToUpper().Contains(request.Search.ToUpper())
                 || c.Data.ToUpper().Contains(request.Search.ToUpper())
                 )
                 &&
                 (request.Level == null || c.Level == request.Level)
                 &&
                 (request.ProcessId == null || c.ProcessId == request.ProcessId)
                 &&
                 (request.Status == null || c.Status == request.Status)
                 &&
                 (c.CreatedBy.ToUpper() == _user.UserName.ToUpper())
                 );
            }
            else
            {

                var upperName = _user.UserName.ToUpper();
                switch (request.Status)
                {
                    case Status.New:
                    case Status.InProcess:
                        dataQuery = await _approvalsRepository.GetAllAsync(c =>
                        c.Status == request.Status && c.RawApprovers.Contains(upperName) &&
                        (string.IsNullOrEmpty(request.Search)
                        || c.LevelName.ToUpper().Contains(request.Search.ToUpper())
                        || c.LevelDescription.ToUpper().Contains(request.Search.ToUpper())
                        || c.Data.ToUpper().Contains(request.Search.ToUpper())
                        )
                        &&
                        (request.Level == null || c.Level == request.Level)
                        );
                        break;
                    case Status.Approved:
                    case Status.Rejected:
                        dataQuery = await _approvalsRepository.GetAllAsync(c =>
                        c.Status == request.Status && c.RawAlreadyApproved.Contains(upperName) &&
                        (string.IsNullOrEmpty(request.Search)
                        || c.LevelName.ToUpper().Contains(request.Search.ToUpper())
                        || c.LevelDescription.ToUpper().Contains(request.Search.ToUpper())
                        || c.Data.ToUpper().Contains(request.Search.ToUpper())
                        )
                        &&
                        (request.Level == null || c.Level == request.Level)
                        );
                        break;
                    case Status.InReview:
                    default:
                        dataQuery = await _approvalsRepository.GetAllAsync(c =>
                        c.RawAlreadyApproved.Contains(upperName) &&
                        (string.IsNullOrEmpty(request.Search)
                        || c.LevelName.ToUpper().Contains(request.Search.ToUpper())
                        || c.LevelDescription.ToUpper().Contains(request.Search.ToUpper())
                        || c.Data.ToUpper().Contains(request.Search.ToUpper())
                        )
                        &&
                        (request.Level == null || c.Level == request.Level)
                        );
                        break;
                };

                // dataQuery = await _approvalsRepository.GetAllAsync(func);          

            }

            var record = await dataQuery.OrderByDescending(x => x.Id).ToPaginatedListAsync(request.PageNumber, request.PageSize);

            record.Message = record.TotalCount > 0 ? "data retrieved ok" : "No data found";

            return Result<PaginatedResult<ProcessView>>.Success(record, "success");
        }
    }
}