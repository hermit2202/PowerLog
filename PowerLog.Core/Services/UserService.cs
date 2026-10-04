using AutoMapper;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.DTOs.User;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AuthService authService;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            AuthService authService)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.authService = authService;
        }

        public async Task<UserDto> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.GetByIdAsync(id, cancellationToken);

            if (user == null)
            {
                throw new Exception("Такой пользователь не найден.");
            }

            return mapper.Map<UserDto>(user);
        }

        public async Task<LoginResponseDto> Login(LoginDto dto, CancellationToken cancellationToken = default)
        {
            var user = await userRepository.GetByEmailAsync(dto.Email, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new Exception("Неверный email или пароль.");
            }

            var token = authService.GenerateToken(user);

            return new LoginResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                JwtToken = token
            };
        }

        public async Task<UserDto> Register(RegisterDto dto, CancellationToken cancellationToken = default)
        {
            var existingUser = await userRepository.GetByEmailAsync(dto.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new Exception("Пользователь с таким email уже существует.");
            }

            var newUser = mapper.Map<User>(dto);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            userRepository.Add(newUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserDto>(newUser);
        }
    }
}
