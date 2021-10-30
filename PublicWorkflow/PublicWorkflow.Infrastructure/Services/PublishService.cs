using PublicWorkflow.Application.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicWorkflow.Infrastructure.Services
{
    public class PublishService : IPublishService
    {
        public PublishService()
        {

        }
        public Task PublishProcess(long processId)
        {
            throw new NotImplementedException();
        }
    }
}
