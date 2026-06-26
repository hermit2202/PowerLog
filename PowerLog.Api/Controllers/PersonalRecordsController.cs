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
        private readonly IRepository<PersonalRecord> repository;

        public PersonalRecordsController(IRepository<PersonalRecord> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalRecordDto>>> GetAll()
        {
            var personalRecords = await repository.GetAllAsync();

            var dtos = personalRecords.Select(p => new PersonalRecordDto
            {
                PersonalRecordId = p.PersonalRecordId,
                UserId = p.UserId,
                ExerciseId = p.ExerciseId,
                Weight = p.Weight,
                Reps = p.Reps,
                RPE = p.RPE,
                RecordDate = p.RecordDate,
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalRecordDto>> GetById(Guid id)
        {
            var personalRecord = await repository.GetByIdAsync(id);
            if (personalRecord == null)
            {
                return NotFound();
            }

            var dto = new PersonalRecordDto
            {
                PersonalRecordId = personalRecord.PersonalRecordId,
                UserId = personalRecord.UserId,
                ExerciseId = personalRecord.ExerciseId,
                Weight = personalRecord.Weight,
                Reps = personalRecord.Reps,
                RPE = personalRecord.RPE,
                RecordDate = personalRecord.RecordDate,
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<PersonalRecordDto>> Create(CreatePersonalRecordDto dto)
        {
            var personalRecord = new PersonalRecord
            {
                UserId = dto.UserId,
                ExerciseId = dto.ExerciseId,
                Weight = dto.Weight,
                Reps = dto.Reps,
                RPE = dto.RPE,
                RecordDate = DateTime.UtcNow,
            };

            await repository.CreateAsync(personalRecord);

            var resultDto = new PersonalRecordDto
            {
                PersonalRecordId = personalRecord.PersonalRecordId,
                UserId = personalRecord.UserId,
                ExerciseId = personalRecord.ExerciseId,
                Weight = personalRecord.Weight,
                Reps = personalRecord.Reps,
                RPE = personalRecord.RPE,
                RecordDate = personalRecord.RecordDate,
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.PersonalRecordId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdatePersonalRecordDto dto)
        {
            var personalRecord = await repository.GetByIdAsync(id);
            if (personalRecord == null || id != personalRecord.PersonalRecordId)
            {
                return BadRequest();
            }

            personalRecord.ExerciseId = dto.ExerciseId;
            personalRecord.Weight = dto.Weight;
            personalRecord.Reps = dto.Reps;
            personalRecord.RPE = dto.RPE;

            await repository.UpdateAsync(personalRecord);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var personalRecord = await repository.GetByIdAsync(id);
            if (personalRecord == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
