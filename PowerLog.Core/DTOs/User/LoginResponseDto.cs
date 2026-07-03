namespace PowerLog.Core.DTOs.User
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string JwtToken { get; set; } = string.Empty;
    }
}
