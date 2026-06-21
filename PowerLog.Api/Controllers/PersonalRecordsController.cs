using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<PersonalRecord>>> GetAll()
        {
            var personalRecords = await repository.GetAllAsync();
            return Ok(personalRecords);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalRecord>> GetById(Guid id)
        {
            var personalRecord = await repository.GetByIdAsync(id);
            if (personalRecord == null)
            {
                return NotFound();
            }
            return Ok(personalRecord);
        }

        [HttpPost]
        public async Task<ActionResult<PersonalRecord>> Create(PersonalRecord personalRecord)
        {
            await repository.CreateAsync(personalRecord);
            return CreatedAtAction(nameof(GetById), new { id = personalRecord.Id }, personalRecord);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PersonalRecord personalRecord)
        {
            if (personalRecord == null || id != personalRecord.Id)
            {
                return BadRequest();
            }
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
