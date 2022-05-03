using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using PublicWorkflow.Application.Interfaces.Repositories;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Threading;
using System.Threading.Tasks;
using PublicWorkflow.Application.Features.Queries.GetAllPaged;

namespace PublicWorkflow.Application.Features.Commands.Create
{
    public partial class UpdateApprovalRuleCommand : CreateUpdateApprovalRule, IRequest<Result<long>>
    {
        public bool? IsDeleted { get; set; }
    }

    public class UpdateApprovalRuleCommandHandler : IRequestHandler<UpdateApprovalRuleCommand, Result<long>>
    {
        private readonly IGenericRepository<ApprovalConfig> _approvalConfigRepository;
        private readonly IGenericRepository<ApprovalRule> _approvalRuleRepository;
        private readonly IMapper _mapper;

        public UpdateApprovalRuleCommandHandler(
            IGenericRepository<ApprovalConfig> approvalConfigRepository, 
            IGenericRepository<ApprovalRule> approvalRuleRepository, 
            IMapper mapper)
        {
            _approvalConfigRepository = approvalConfigRepository;
            _approvalRuleRepository = approvalRuleRepository;
            _mapper = mapper;
        }

        public async Task<Result<long>> Handle(UpdateApprovalRuleCommand request, CancellationToken cancellationToken)
        {
            var found = await _approvalRuleRepository.GetAsync(c => c.Id == request.Id);

            if (found==null)
            {
                return await Result<long>.FailAsync("Rule does not exist");
            }
            else
            {
                found.Name = request.Name ?? found.Name;
                found.IsDeleted = request.IsDeleted ?? found.IsDeleted;
                found.Name = request.Name ?? found.Name;
                found.Values = request.Values ?? found.Values;
                found.Type = request.Type ?? found.Type;
                found.Action = request.Action?? found.Action;
                found.Condition = request.Condition??found.Condition;

                await _approvalRuleRepository.UpdateAsync(found);

                return await Result<long>.SuccessAsync(found.Id);
            }
        }
    }
}