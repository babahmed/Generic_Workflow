namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class Organization : AuditableExt
    {
        public string Name { get; set; }
        public string Motto { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string ContactEmail { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string LandMark { get; set; }
        public string Phone { get; set; }
    }
}
