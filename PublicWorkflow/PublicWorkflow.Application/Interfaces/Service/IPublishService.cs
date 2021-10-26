using System.Threading.Tasks;

namespace PublicWorkflow.Application.Interfaces.Service
{
    public interface IPublishService
    {
        Task PublishProcess(long processId);
    }
}
