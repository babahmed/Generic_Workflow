using PublicWorkflow.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class ProcessView : AuditableExt
    {
        public long ProcessId { get; set; }
        public string[] Attachements { set; get; }
        public string JobReferenceId { set; get; }
        public string Data { set; get; }
        public Status ProcessStatus { get; set; }
        public Status Status { get; set; }
        public DateTime? Logged { get; set; }
        public string[] AlreadyApproved { get; set; }
        public string[] Comments { get; set; }
        public string LevelName { get; set; }
        public string LevelDescription { get; set; }
        public int Level { get; set; }
        public int RequiredApprovers { get; set; }
        public string[] Approvers { get; set; }
        public string ProcessName { get; set; }
        public string ProcessDescription { get; set; }
    }
}
