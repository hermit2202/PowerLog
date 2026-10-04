using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.PersonalRecord;
using PowerLog.Core.Interfaces;

namespace PowerLog.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalRecordsController : ControllerBase
    {
        private readonly IPersonalRecordService personalRecordService;

        public PersonalRecordsController(IPersonalRecordService personalRecordService)
        {
            this.personalRecordService = personalRecordService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalRecordDto>>> GetAll(Guid exerciseId, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.Parse(
                    User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("User ID not found in token.")
                );

                var personalRecords = await personalRecordService.GetAllPersonalRecordsAsync(userId, exerciseId, cancellationToken);
                return Ok(personalRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении персональных рекордов: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalRecordDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var dto = await personalRecordService.GetPersonalRecordByIdAsync(id, cancellationToken);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonalRecordDto>> Create(CreatePersonalRecordDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.Parse(
                    User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("User ID not found in token.")
                );

                var personalRecord = await personalRecordService.CreatePersonalRecordAsync(dto, userId, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = personalRecord.PersonalRecordId }, personalRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ошибка при создании персонального рекорда: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePersonalRecordDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await personalRecordService.UpdatePersonalRecordAsync(dto, id, cancellationToken);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var success = await personalRecordService.DeletePersonalRecordAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
