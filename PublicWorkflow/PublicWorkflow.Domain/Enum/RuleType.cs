namespace PublicWorkflow.Domain.Enum
{
    public enum RuleType
    {
        User = 0, Count, Value
    }
    public enum RuleCondition
    {
        Must = 0, Can , Greater
    }
    public enum RuleAction
    {
        Approve, Reject
    }
}
