namespace PowerLog.Core.Models;

public class CoachClient
{
    public Guid Id { get; set; }
    public Guid CoachId { get; set; }
    public Guid ClientId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User Coach { get; set; }
    public User Client { get; set; }
}
