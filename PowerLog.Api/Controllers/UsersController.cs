using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerLog.Api.Extensions;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.DTOs.User;

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
        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetMe(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var dto = await userService.GetById(userId, cancellationToken);
            return Ok(dto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto, CancellationToken cancellationToken)
        {
            var userDto = await userService.Register(dto, cancellationToken);
            return CreatedAtAction(nameof(GetMe), null, userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginDto dto, CancellationToken cancellationToken)
        {
            var response = await userService.Login(dto, cancellationToken);
            return Ok(response);
        }
    }
}
