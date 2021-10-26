namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class OrganizationUser : AuditableExt
    {
        public string Email { get; set; }
        public Organization Organization { get; set; }

    }
}
