using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PublicWorkflow.Application.DTOs.ViewModel;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Application.Interfaces.Shared;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        private readonly IAuthenticatedUserService _user;
        private readonly IMediator _mediator;

        public CreateQuickProcessConfigCommandHandler(
            IGenericRepository<ProcessConfig> ProcessConfigRepository,
            IHttpContextAccessor httpContextAccessor,
            IMediator _mediator,
            IAuthenticatedUserService _user,
            IMapper mapper)
        {
            _ProcessConfigRepository = ProcessConfigRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            this._mediator = _mediator;
            this._mediator = _mediator;

        }

        public async Task<Result<long>> Handle(CreateQuickProcessConfigCommand request, CancellationToken cancellationToken)
        {
            var config = _mapper.Map<ProcessConfig>(request);
            config.OrganizationId = _user.OId;
            config.UserId = _user.UId;
            config.RequiredApprovalLevels = request.ApprovalLevels.Count;

            await _ProcessConfigRepository.AddAsync(config);



            foreach (var ApprovalItem in request.ApprovalLevels)
            {
                var item = _mapper.Map<CreateApprovalConfigCommand>(ApprovalItem);
                item.ProcessConfigId = config.Id;
                item.Name = $"Level {item.Level}";
                item.Description = $"Approval at Level {item.Level}";
                item.Approvers = item.Approvers.Select(s => s.ToUpperInvariant()).ToArray();
                await _mediator.Send(item);
            }

            return Result<long>.Success(config.Id);
        }
    }
}