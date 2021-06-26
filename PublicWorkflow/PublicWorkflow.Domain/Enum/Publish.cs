using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Enum
{
    public enum Publish
    {
        None = 0, MailOnly,GetOnly, PostOnly,GetMail,PostMail
    }
}
