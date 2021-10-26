namespace PublicWorkflow.Application.DTOs.ViewModel
{
    public class ApprovalConfigBase
    {
        public int Level { get; set; }
        public int RequiredApprovers { get; set; }
        public string[] Approvers { get; set; }
    }
}
