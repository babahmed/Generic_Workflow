using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Extensions;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using PublicWorkflow.Domain.Enum;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class UpdateProcessRuleCommand : IRequest<Result<long>>
    {
        public long RuleId { get; set; }
        public bool? IsDeleted { get; set; }
        public string Name { get; set; }
        public string[] Values { get; set; }
        public string Type { get; set; }
        public string Condition { get; set; }
        public string Action { get; set; }
    }

    public class UpdateProcessRuleCommandHandler : IRequestHandler<UpdateProcessRuleCommand, Result<long>>
    {
        private readonly IGenericRepository<ApprovalConfig> _approvalConfigRepository;
        private readonly IGenericRepository<ProcessRule> _processRuleRepository;
        private readonly IMapper _mapper;

        public UpdateProcessRuleCommandHandler(
            IGenericRepository<ApprovalConfig> approvalConfigRepository,
            IGenericRepository<ProcessRule> _processRuleRepository,
            IMapper mapper)
        {
            _approvalConfigRepository = approvalConfigRepository;
            this._processRuleRepository = _processRuleRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(UpdateProcessRuleCommand request, CancellationToken cancellationToken)
        {
            var found = await _processRuleRepository.GetAsync(c => c.Id == request.RuleId);

            if (found == null)
            {
                return Result<long>.Fail("Rule does not exist");
            }
            else
            {
                found.Name = request.Name ?? found.Name;
                found.IsDeleted = request.IsDeleted ?? found.IsDeleted;
                found.Name = request.Name ?? found.Name;
                found.Values = request.Values ?? found.Values;
                found.Type = request.Type == null ? request.Type.ToEnum<RuleType>() : found.Type;
                found.Action = request.Action == null ? request.Action.ToEnum<RuleAction>() : found.Action;
                found.Condition = request.Condition == null ? request.Condition.ToEnum<Rulecondition>() : found.Condition;

                await _processRuleRepository.UpdateAsync(found);

                return Result<long>.Success(found.Id);
            }
        }
    }
}