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
    public partial class CreateProcessRuleCommand : GetAllProcessRuleResponse,IRequest<Result<long>>
    {

    }

    public class CreateProcessRuleCommandHandler : IRequestHandler<CreateProcessRuleCommand, Result<long>>
    {
        private readonly IGenericRepository<ProcessConfig> _processConfigRepository;
        private readonly IGenericRepository<ProcessRule> _processRuleRepository;
        private readonly IMapper _mapper;

        public CreateProcessRuleCommandHandler(
            IGenericRepository<ProcessConfig> processConfigRepository, 
            IGenericRepository<ProcessRule> processRuleRepository, 
            IMapper mapper)
        {
            _processConfigRepository = processConfigRepository;
            _processRuleRepository = processRuleRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(CreateProcessRuleCommand request, CancellationToken cancellationToken)
        {
            var type = request.Type.ToEnum<RuleType>();
            var cond = request.Condition.ToEnum<Rulecondition>();
            var action = request.Action.ToEnum<RuleAction>();
            var existing = await _processRuleRepository.GetAsync(c => c.ProcessConfigId == request.ProcessConfigId
            && c.Name == request.Name
            && !c.IsDeleted
            && c.Type == type
            && c.Condition == cond
            && c.Action == action
            );

            if (existing!=null)
            {
                return Result<long>.Fail("Rule type already exist");
            }

            var rec = _mapper.Map<ProcessRule>(request);
            await _processRuleRepository.AddAsync(rec);
            return Result<long>.Success(rec.Id,"rule added successfully");
        }
    }
}