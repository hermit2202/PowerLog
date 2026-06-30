using PowerLog.Core.DTOs.User;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task<UserDto> GetById(Guid id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Такой пользователь не найден.");
            }

            var dto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
            };

            return dto;
        }

        public async Task<UserDto> Login(LoginDto dto)
        {
            var existingUser = (await repository.GetAllAsync())
                .FirstOrDefault(u => u.Email == dto.Email);

            if (existingUser == null)
            {
                throw new Exception("Неверный email или пароль.");
            }

            var verify = BCrypt.Net.BCrypt.Verify(dto.Password, existingUser.PasswordHash);

            if (verify == false)
            {
                throw new Exception("Неверный email или пароль.");
            }

            var userDto = new UserDto
            {
                UserId = existingUser.UserId,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
            };

            return userDto;
        }

        public async Task<UserDto> Register(RegisterDto dto)
        {
            var existingUser = (await repository.GetAllAsync())
                .FirstOrDefault(u => u.Email == dto.Email);

            if (existingUser != null)
            {
                throw new Exception("Пользователь с таким email уже существует.");
            }

            var newUser = new User
            {
                UserId = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };

            await repository.CreateAsync(newUser);

            var userDto = new UserDto
            {
                UserId = newUser.UserId,
                UserName = dto.UserName,
                Email = dto.Email,
            };

            return userDto;
        }
    }
}
