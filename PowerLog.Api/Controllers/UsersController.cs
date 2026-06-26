using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.User;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> repository;

        public UsersController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var dto = new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
            };

            return Ok(dto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var existingUser = (await repository.GetAllAsync())
                .FirstOrDefault(u => u.Email == dto.Email);

            if (existingUser != null)
            {
                return Conflict("Пользователь с таким email уже существует.");
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

            return CreatedAtAction(nameof(GetById), new { id = newUser.UserId }, userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var existingUser = (await repository.GetAllAsync())
                .FirstOrDefault(u => u.Email == dto.Email);

            if (existingUser == null)
            {
                return Unauthorized("Неверный email или пароль.");
            }

            var verify = BCrypt.Net.BCrypt.Verify(dto.Password, existingUser.PasswordHash);

            if (verify == false)
            {
                return Unauthorized("Неверный email или пароль.");
            }

            var userDto = new UserDto
            {
                UserId = existingUser.UserId,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
            };

            return Ok(userDto);
        }
    }
}
