using PublicWorkflow.Application.DTOs.Mail;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}