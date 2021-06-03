using PublicWorkflow.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Interfaces.Repositories
{
    public interface ILogRepository
    {
        Task<List<AuditLogResponse>> GetAuditLogsAsync(string userId);

        Task AddLogAsync(string action, string userId);
    }
}