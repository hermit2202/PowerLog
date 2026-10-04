using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.User;
using PowerLog.Core.Interfaces;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var dto = await userService.GetById(id, cancellationToken);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var userDto = await userService.Register(dto, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = userDto.UserId }, userDto);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var response = await userService.Login(dto, cancellationToken);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
