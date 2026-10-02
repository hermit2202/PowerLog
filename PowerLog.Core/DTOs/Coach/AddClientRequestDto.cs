using System.ComponentModel.DataAnnotations;

namespace PowerLog.Core.DTOs.Coach;

public class AddClientRequestDto
{
    [Required]
    public Guid ClientId { get; set; }
}
