using PublicWorkflow.Domain.Enum;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class ApprovalRule : AuditableExt
    {
        public string Name { get; set; }
        public string[] Values { get; set; }
        public RuleType Type { get; set; }
        public Rulecondition Condition { get; set; }
        public RuleAction Action { get; set; }
        public ApprovalConfig ApprovalConfig { get; set; }
        public long ApprovalConfigId { get; set; }
    }
}
