using AspNetCoreHero.Abstractions.Domain;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class AuditableExt : AuditableEntity
    {
        public new long Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
