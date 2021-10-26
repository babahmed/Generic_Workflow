using PublicWorkflow.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class ProcessRule : AuditableExt
    {
        public string Name { get; set; }
        public string[] Values { get; set; }
        public RuleType Type { get; set; }
        public Rulecondition Condition { get; set; }
        public RuleAction Action { get; set; }
        public ProcessConfig ProcessConfig { get; set; }
        public long ProcessConfigId { get; set; }
    }
}
