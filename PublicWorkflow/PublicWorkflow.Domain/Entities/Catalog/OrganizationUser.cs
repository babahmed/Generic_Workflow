using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class OrganizationUser : AuditableExt
    {
        public string Email { get; set; }
        public Organization Organization { get; set; }

    }
}
