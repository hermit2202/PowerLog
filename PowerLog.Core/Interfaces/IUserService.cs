using PowerLog.Core.DTOs.User;

namespace PowerLog.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id);
        Task<UserDto> Register(RegisterDto dto);
        Task<LoginResponseDto> Login(LoginDto dto);
    }
}
