namespace PublicWorkflow.Application.Features.Queries.GetAllPaged
{
    public class GetAllApprovalConfigResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ProcessConfigId { get; set; }
        public int Level { get; set; }
        public int RequiredApprovers { get; set; }
        public string[] Approvers { get; set; }
    }
}