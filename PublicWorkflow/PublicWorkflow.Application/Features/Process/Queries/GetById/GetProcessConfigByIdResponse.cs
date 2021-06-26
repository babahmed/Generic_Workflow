using PublicWorkflow.Domain.Enum;

namespace PublicWorkflow.Application.Features.Queries.GetById
{
    public class GetProcessConfigByIdResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequiredApprovalLevels { get; set; }
        public string PublishType { get; set; }
        public string FeedBackUrl { get; set; }
        public bool SingleRejection { get; set; }
        public bool NotifyAllApproverOnApproval { get; set; }
        public bool NotifyInitiatorOnApproval { get; set; }
        public bool AttachApprovalPdf { get; set; }
        public bool IncludeApproverDetails { get; set; }
        public bool RequiresAllLevelsForRejection { get; set; }
    }
}