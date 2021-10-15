using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using PublicWorkflow.Application.DTOs.ViewModel;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class CreateQuickProcessConfigCommand : IRequest<Result<long>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApprovalConfigBase> ApprovalLevels { get; set; }
    
    }

    public class CreateQuickProcessConfigCommandHandler : IRequestHandler<CreateQuickProcessConfigCommand, Result<long>>
    {
        private readonly IGenericRepository<ProcessConfig> _ProcessConfigRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public CreateQuickProcessConfigCommandHandler(
            IGenericRepository<ProcessConfig> ProcessConfigRepository,
            IHttpContextAccessor httpContextAccessor,
            IMediator _mediator,
            IMapper mapper)
        {
            _ProcessConfigRepository = ProcessConfigRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            this._mediator = _mediator;

        }

        public async Task<Result<long>> Handle(CreateQuickProcessConfigCommand request, CancellationToken cancellationToken)
        {
            var config = _mapper.Map<ProcessConfig>(request);
            config.OrganizationId= _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("OrganizationId"))!=null?
            long.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("OrganizationId")).Value):null;
            config.UserId= _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("UserId")) != null ?
            long.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type.Contains("UserId")).Value) : null;

            await _ProcessConfigRepository.AddAsync(config);

            var approvalCommands = _mapper.Map<List<CreateApprovalConfigCommand>>(request.ApprovalLevels);

            foreach (var item in approvalCommands)
            {
                item.ProcessConfigId = config.Id;
                item.Name = $"Level {item.Level}";
                item.Description = $"Approval at Level {item.Level}";
                await _mediator.Send(item);
            }

            return Result<long>.Success(config.Id);
        }
    }
}