using PublicWorkflow.Application.Features.Commands.Update;
using PublicWorkflow.Application.Interfaces.Shared;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Interfaces.Service
{
    public interface IProcessService
    {
        Task PostApproval(UpdateLevelCommand request, IAuthenticatedUserService user);
    }
}
