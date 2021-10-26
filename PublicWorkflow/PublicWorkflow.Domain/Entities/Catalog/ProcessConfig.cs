using System;
using System.Collections.Generic;

namespace PublicWorkflow.Domain.Entities.Catalog
{
    public class ProcessConfig : AuditableExt
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovalLevels { get; set; }
        public bool IsEnabled { get; set; }
        public virtual ICollection<ApprovalConfig> ApprovalConfigs { get; set; }
        public virtual ICollection<ProcessRequirement> Requirements { get; set; }
        public virtual ICollection<PublishOption> PublishConfigs { get; set; }
        public virtual ICollection<ProcessRule> Rules { get; set; }
        public long? OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public Guid? UserId { get; set; }
    }
}
