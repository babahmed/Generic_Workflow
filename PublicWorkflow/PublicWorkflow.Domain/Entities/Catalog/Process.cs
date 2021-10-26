using PublicWorkflow.Domain.Enum;
using System;
using System.Collections.Generic;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class Process : AuditableExt
    {
        public ProcessConfig ProcessConfig { get; set; }
        public long ProcessConfigId { set; get; }
        public string[] Attachements { set; get; }
        public string JobReferenceId { set; get; }
        public string Data { set; get; }
        public bool IsPublished { set; get; }
        public virtual ICollection<Approval> Actions { get; set; }
        public DateTime? Completed { get; set; }
        public Status Status { get; set; }
    }
}
