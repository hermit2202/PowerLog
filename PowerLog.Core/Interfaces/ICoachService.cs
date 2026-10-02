using PowerLog.Core.DTOs.Coach;
using PowerLog.Core.Models;

namespace PowerLog.Core.Interfaces;

public interface ICoachService
{
    Task<IEnumerable<ClientDto>> GetClientsAsync(Guid coachId);
    Task<bool> AddClientAsync(Guid coachId, Guid clientId);
    Task<bool> RemoveClientAsync(Guid coachId, Guid clientId);
    Task<bool> IsCoachForAsync(Guid coachId, Guid clientId);
}
