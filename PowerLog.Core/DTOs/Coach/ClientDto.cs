using System.ComponentModel.DataAnnotations;

namespace PowerLog.Core.DTOs.Coach;

public class ClientDto
{
    [Required]
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
}
