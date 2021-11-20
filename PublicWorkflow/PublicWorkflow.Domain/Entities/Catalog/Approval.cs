using PublicWorkflow.Domain.Enum;
using System;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class Approval : AuditableExt
    {
        public bool Treated { get; set; }
        public Status Status { get; set; }
        public Status ReviewStatus { get; set; }
        public DateTime? ReviewStarted { get; set; }
        public DateTime? Actioned { get; set; }
        public long ProcessId { get; set; }
        public Process Process { get; set; }
        public long ApprovalconfigId { get; set; }
        public ApprovalConfig ApprovalConfig { get; set; }
        public string[] AlreadyApproved { get; set; }
        public string[] AlreadyActioned { get; set; }
        public string[] Comments { get; set; }
    }
}
