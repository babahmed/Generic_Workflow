using PublicWorkflow.Domain.Enum;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class ProcessRule : AuditableExt
    {
        public string Name { get; set; }
        public string[] Values { get; set; }
        public RuleType Type { get; set; }
        public RuleCondition Condition { get; set; }
        public RuleAction Action { get; set; }
        public ProcessConfig ProcessConfig { get; set; }
        public long ProcessConfigId { get; set; }
    }
}
