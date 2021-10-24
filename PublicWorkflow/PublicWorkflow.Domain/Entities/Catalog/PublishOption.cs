using PublicWorkflow.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class PublishOption : AuditableExt
    {
        public Publish Publish { get; set; }
        public string Url_Topic { get; set; }
        public string PostObjectKeyNames { get; set; }
    }
}
