using PublicWorkflow.Domain.Enum;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class ProcessRequirement : AuditableExt
    {
        public Requirement Requirement { get; set; }
    }
}
