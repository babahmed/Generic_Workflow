using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class CreateApprovalRuleCommand : CreateUpdateApprovalRule,IRequest<Result<long>>
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
            var existing = (await _approvalRuleRepository.GetAsync(c => c.ApprovalConfigId == request.ApprovalConfigId 
                                                                        && c.Name==request.Name 
                                                                        && !c.IsDeleted 
                                                                        && c.Type == request.Type
                                                                        && c.Condition == request.Condition
                                                                        && c.Action == request.Action
            ));

            if (existing!=null)
            {
                return await Result<long>.FailAsync("Rule type already exist");
            }

            var rec = _mapper.Map<ApprovalRule>(request);
            await _approvalRuleRepository.AddAsync(rec);
            return await Result<long>.SuccessAsync(rec.Id,"rule added successfully");
        }
    }
}