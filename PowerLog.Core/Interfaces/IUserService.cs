using PowerLog.Core.DTOs.User;

namespace PowerLog.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id);
        Task<UserDto> Register(RegisterDto dto);
        Task<UserDto> Login(LoginDto dto);
    }
}
