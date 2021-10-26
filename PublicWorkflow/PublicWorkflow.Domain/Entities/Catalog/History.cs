namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class History : AuditableExt
    {
        public long? ProcessId { get; set; }
        public long? ApprovalId { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }

    }
}
