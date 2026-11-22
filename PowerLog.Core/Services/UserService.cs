using AutoMapper;
using PowerLog.Core.Contracts.IData;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.DTOs.User;
using PowerLog.Core.Exceptions;
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
                throw new NotFoundException("Такой пользователь не найден.");
            }

            return mapper.Map<UserDto>(user);
        }

        public async Task<LoginResponseDto> Login(LoginDto dto, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = NormalizeEmail(dto.Email);
            var user = await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedException("Неверный email или пароль.");
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
            var normalizedEmail = NormalizeEmail(dto.Email);
            var existingUser = await userRepository.GetByEmailAsync(normalizedEmail, cancellationToken);

            if (existingUser != null)
            {
                throw new ConflictException("Пользователь с таким email уже существует.");
            }

            var newUser = mapper.Map<User>(dto);
            newUser.Email = normalizedEmail;
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            userRepository.Add(newUser);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<UserDto>(newUser);
        }

        private static string NormalizeEmail(string email)
        {
            return email.ToLowerInvariant().Trim();
        }
    }
}
