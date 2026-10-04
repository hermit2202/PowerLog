using PowerLog.Core.DTOs.User;

namespace PowerLog.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<UserDto> Register(RegisterDto dto, CancellationToken cancellationToken = default);
        Task<LoginResponseDto> Login(LoginDto dto, CancellationToken cancellationToken = default);
    }
}
