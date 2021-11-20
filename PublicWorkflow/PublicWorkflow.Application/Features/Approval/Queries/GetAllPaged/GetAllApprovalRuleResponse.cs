namespace PublicWorkflow.Application.Features.Queries.GetAllPaged
{
    public class GetAllApprovalRuleResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string[] Values { get; set; }
        public string Type { get; set; }
        public string Condition { get; set; }
        public string Action { get; set; }
        public long ApprovalConfigId { get; set; }
    }
}