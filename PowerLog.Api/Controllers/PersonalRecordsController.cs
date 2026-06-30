using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.PersonalRecord;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalRecordsController : ControllerBase
    {
        private readonly IExerciseService personalRecordService;

        public PersonalRecordsController(IExerciseService personalRecordService)
        {
            this.personalRecordService = personalRecordService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalRecordDto>>> GetAll(Guid exerciseId)
        {
            try
            {
                var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                var personalRecords = await personalRecordService.GetAllPersonalRecordsAsync(userId, exerciseId);
                return Ok(personalRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при полчении персональных рекордов: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalRecordDto>> GetById(Guid id)
        {
            try
            {
                var dto = await personalRecordService.GetPersonalRecordByIdAsync(id);
                return Ok(dto);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonalRecordDto>> Create(CreatePersonalRecordDto dto)
        {
            try
            {
                var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                var personalRecord = await personalRecordService.CreatePersonalRecordAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = personalRecord.PersonalRecordId }, personalRecord);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ошибка при создании пресонального рекорда: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePersonalRecordDto dto)
        {
            try
            {
                var personalRecord = await personalRecordService.UpdatePersonalRecordAsync(dto, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await personalRecordService.DeletePersonalRecordAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
