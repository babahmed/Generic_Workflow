using PublicWorkflow.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string[] EligibleApprover { get; set; }
        public string[] AlreadyApproved { get; set; }
        public string[] Comments { get; set; }
    }
}
