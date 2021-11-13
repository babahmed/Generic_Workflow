using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Extensions;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using PublicWorkflow.Domain.Enum;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class CreateApprovalRuleCommand : GetAllApprovalRuleResponse,IRequest<Result<long>>
    {
     
    }

    public class CreateApprovalRuleCommandHandler : IRequestHandler<CreateApprovalRuleCommand, Result<long>>
    {
        private readonly IGenericRepository<ApprovalConfig> _approvalConfigRepository;
        private readonly IGenericRepository<ApprovalRule> _approvalRuleRepository;
        private readonly IMapper _mapper;

        public CreateApprovalRuleCommandHandler(
            IGenericRepository<ApprovalConfig> approvalConfigRepository, 
            IGenericRepository<ApprovalRule> approvalRuleRepository, 
            IMapper mapper)
        {
            _approvalConfigRepository = approvalConfigRepository;
            _approvalRuleRepository = approvalRuleRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(CreateApprovalRuleCommand request, CancellationToken cancellationToken)
        {
            var type = request.Type.ToEnum<RuleType>();
            var cond = request.Condition.ToEnum<Rulecondition>();
            var action = request.Action.ToEnum<RuleAction>();
            var existing = (await _approvalRuleRepository.GetAsync(c => c.ApprovalConfigId == request.ApprovalConfigId 
            && c.Name==request.Name 
            && !c.IsDeleted 
            && c.Type == type
            && c.Condition == cond
            && c.Action == action
            ));

            if (existing!=null)
            {
                return Result<long>.Fail("Rule type already exist");
            }

            var rec = _mapper.Map<ApprovalRule>(request);
            await _approvalRuleRepository.AddAsync(rec);
            return Result<long>.Success(rec.Id,"rule added successfully");
        }
    }
}