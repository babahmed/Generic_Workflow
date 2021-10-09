using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Domain.Enum
{
    public enum Publish
    {
        Mail,
        Get,
        Post,
        Kafka,
        RabbitMQ
    }
}
