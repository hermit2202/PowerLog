using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerLog.Api.Extensions;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.DTOs.PersonalRecord;

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
            var userId = User.GetUserId();

            var personalRecords = await personalRecordService.GetAllPersonalRecordsAsync(userId, exerciseId, cancellationToken);
            return Ok(personalRecords);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalRecordDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var dto = await personalRecordService.GetPersonalRecordByIdAsync(id, userId, cancellationToken);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PersonalRecordDto>> Create(CreatePersonalRecordDto dto, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var personalRecord = await personalRecordService.CreatePersonalRecordAsync(dto, userId, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = personalRecord.PersonalRecordId }, personalRecord);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePersonalRecordDto dto, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            await personalRecordService.UpdatePersonalRecordAsync(dto, id, userId, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var success = await personalRecordService.DeletePersonalRecordAsync(id, userId, cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
