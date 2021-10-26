using PublicWorkflow.Domain.Enum;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class PublishOption : AuditableExt
    {
        public Publish Publish { get; set; }
        public string Url_Topic { get; set; }
        public string PostObjectKeyNames { get; set; }
    }
}
