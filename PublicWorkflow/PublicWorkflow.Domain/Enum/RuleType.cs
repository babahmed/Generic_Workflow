using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Enum
{
    public enum RuleType
    {
        User=0,Count
    }
    public enum Rulecondition
    {
        Must = 0, Should , Can
    }
    public enum RuleAction
    {
        Approve, Reject
    }
}
