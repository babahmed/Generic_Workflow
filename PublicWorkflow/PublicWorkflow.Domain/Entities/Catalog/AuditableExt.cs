using AspNetCoreHero.Abstractions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class AuditableExt : AuditableEntity
    {
        public new long Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
