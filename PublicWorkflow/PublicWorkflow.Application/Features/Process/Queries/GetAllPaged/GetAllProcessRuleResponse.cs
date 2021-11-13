namespace PublicWorkflow.Application.Features.Queries.GetAllPaged
{
    public class GetAllProcessRuleResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string[] Values { get; set; }
        public string Type { get; set; }
        public string Condition { get; set; }
        public string Action { get; set; }
        public long ProcessConfigId { get; set; }
    }
}