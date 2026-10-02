using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.Coach;
using PowerLog.Core.Interfaces;

namespace PowerLog.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachService coachService;

        public CoachesController(ICoachService coachService)
        {
            this.coachService = coachService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var user = Guid.Parse(
                    User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ??
                    throw new InvalidOperationException("User id not found in token."));

                var clients = await coachService.GetClientsAsync(user);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении клиентов: {ex.Message}");
            }
        }

        [HttpPost("clients")]
        public async Task<IActionResult> AddClient([FromBody] AddClientRequestDto request)
        {
            try
            {
                var user = Guid.Parse(
                    User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ??
                    throw new InvalidOperationException("User id not found in token."));

                var isCreated = await coachService.AddClientAsync(user, request.ClientId);
                if (isCreated != true)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при создании клиента: {ex.Message}");
            }
        }

        [HttpDelete("clients/{clientId}")]
        public async Task<IActionResult> DeleteClient(Guid clientId)
        {
            var user = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
                ??
                throw new InvalidOperationException("User id not found in token."));

            var isDeleted = await coachService.RemoveClientAsync(user, clientId);

            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("clients/{clientId}/is-active")]
        public async Task<IActionResult> IsCoachFor(Guid clientId)
        {
            var coachId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
                ??
                throw new InvalidOperationException("User id not found in token."));

            var isCoach = await coachService.IsCoachForAsync(coachId, clientId);
            return Ok(isCoach);

        }
    }
}
