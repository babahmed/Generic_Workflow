using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class ApprovalConfig : AuditableExt
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long ProcessConfigId { get; set; }
        public ProcessConfig ProcessConfig { get; set; }
        public int Level { get; set; }
        public int RequiredApprovers { get; set; }
        public string[] Approvers { get; set; }
        public virtual ICollection<Approval> Approvals { get; set; }
    }
}
