using PowerLog.Core.DTOs.Coach;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services;

public class CoachService : ICoachService
{
    private readonly IRepository<CoachClient> coachServiceRepository;
    private readonly IRepository<User> userRepository;

    public CoachService(IRepository<CoachClient> coachServiceRepository, IRepository<User> userRepository)
    {
        this.coachServiceRepository = coachServiceRepository;
        this.userRepository = userRepository;
    }

    public async Task<IEnumerable<ClientDto>> GetClientsAsync(Guid coachId)
    {
        var allCoachClients = await coachServiceRepository.GetAllAsync();

        var coachClients = allCoachClients
            .Where(cc => cc.CoachId == coachId)
            .ToList();

        var result = new List<ClientDto>();
        foreach (var cc in coachClients)
        {
            var user = await userRepository.GetByIdAsync(cc.ClientId);
            if (user != null)
            {
                result.Add(new ClientDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = cc.CreatedAt
                });
            }
        }

        return result;
    }

    public async Task<bool> AddClientAsync(Guid coachId, Guid clientId)
    {
        if (coachId == Guid.Empty || clientId == Guid.Empty)
        {
            return false;
        }

        var coach = await userRepository.GetByIdAsync(coachId);
        var client = await userRepository.GetByIdAsync(clientId);
        if (coach == null || client == null)
        {
            return false;
        }

        var allCoachClients = await coachServiceRepository.GetAllAsync();

        var existingConnection = allCoachClients
            .FirstOrDefault(cc => cc.CoachId == coachId && cc.ClientId == clientId);

        if (existingConnection != null)
        {
            return false;
        }

        var newClient = new CoachClient
        {
            Id =  Guid.NewGuid(),
            CoachId = coachId,
            ClientId = clientId,
            CreatedAt = DateTime.UtcNow,
        };

        await coachServiceRepository.CreateAsync(newClient);

        return true;
    }

    public async Task<bool> RemoveClientAsync(Guid coachId, Guid clientId)
    {
        var allCoachClients = await coachServiceRepository.GetAllAsync();
        var existingCoachClient = allCoachClients
            .FirstOrDefault(cc => cc.CoachId == coachId && cc.ClientId == clientId);

        if (existingCoachClient == null)
        {
            return false;
        }

        await coachServiceRepository.DeleteAsync(existingCoachClient.Id);

        return true;
    }

    public async Task<bool> IsCoachForAsync(Guid coachId, Guid clientId)
    {
        var allCoachClients = await coachServiceRepository.GetAllAsync();

        return allCoachClients.Any(cc => cc.CoachId == coachId && cc.ClientId == clientId);
    }
}
