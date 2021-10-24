
using System.ComponentModel;

namespace PublicWorkflow.Domain.Enum
{
    public enum Requirement
    {
        [Description("Reject job on single rejection")]
        SingleRejection = 1,
        [Description("Notify all approver once job is completed")]
        NotifyAllApproverOnCompletion,
        [Description("Notify intiator once job is completed")]
        NotifyInitiatorOnCompletion,
        [Description("Attach approval as pdf in notification")]
        AttachApprovalPdfToNotification,
        [Description("Include the datails of approvers in notification")]
        IncludeApproverDetails
    }
}
