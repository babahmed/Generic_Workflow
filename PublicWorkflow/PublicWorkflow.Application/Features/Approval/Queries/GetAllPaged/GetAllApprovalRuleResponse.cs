using PublicWorkflow.Domain.Enum;

namespace PublicWorkflow.Application.Features.Queries.GetAllPaged
{
    public class GetAllApprovalRuleResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string[] Values { get; set; }
        public string Type { get; set; }
        public string Condition { get; set; }
        public string Action { get; set; }
        public long ApprovalConfigId { get; set; }
    }

    public class CreateUpdateApprovalRule
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string[] Values { get; set; }
        public RuleType? Type { get; set; }
        public RuleCondition? Condition { get; set; }
        public RuleAction? Action { get; set; }
        public long ApprovalConfigId { get; set; }
    }
}