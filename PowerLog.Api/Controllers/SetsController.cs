using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.Set;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetsController : ControllerBase
    {
        private readonly IRepository<Set> repository;

        public SetsController(IRepository<Set> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SetDto>>> GetAll()
        {
            var sets = await repository.GetAllAsync();

            var dtos = sets.Select(s => new SetDto
            {
                SetId = s.SetId,
                Reps = s.Reps,
                Weight = s.Weight,
                RPE = s.RPE,
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SetDto>> GetById(Guid id)
        {
            var set = await repository.GetByIdAsync(id);
            if (set == null)
            {
                return NotFound();
            }

            var dto = new SetDto
            {
                SetId = set.SetId,
                Reps = set.Reps,
                Weight = set.Weight,
                RPE = set.RPE,
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<SetDto>> Create(CreateSetDto dto)
        {
            var set = new Set
            {
                Reps = dto.Reps,
                Weight = dto.Weight,
                RPE = dto.RPE,
            };

            await repository.CreateAsync(set);

            var resultDto = new SetDto
            {
                SetId = set.SetId,
                Reps = set.Reps,
                Weight = set.Weight,
                RPE = set.RPE,
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.SetId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSetDto dto)
        {
            var set = await repository.GetByIdAsync(id);
            if (set == null || id != set.SetId)
            {
                return BadRequest();
            }

            set.Reps = dto.Reps;
            set.Weight = dto.Weight;
            set.RPE = dto.RPE;

            await repository.UpdateAsync(set);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var set = await repository.GetByIdAsync(id);
            if (set == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
