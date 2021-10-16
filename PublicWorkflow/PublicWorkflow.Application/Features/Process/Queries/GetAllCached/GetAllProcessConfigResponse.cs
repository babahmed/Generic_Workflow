using System;

namespace PublicWorkflow.Application.Features.Queries.GetAll
{
    public class GetAllProcessConfigResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovalLevels { get; set; }
        public bool IsEnabled { get; set; }
        public long? OrganizationId { get; set; }
        public Guid? UserId { get; set; }
    }
}