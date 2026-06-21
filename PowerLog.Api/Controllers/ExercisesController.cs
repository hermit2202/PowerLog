using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly IRepository<Exercise> repository;

        public ExercisesController(IRepository<Exercise> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetAll()
        {
            var exercises = await repository.GetAllAsync();
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exercise>> GetById(Guid id)
        {
            var exercise = await repository.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<ActionResult<Exercise>> Create(Exercise exercise)
        {
            await repository.CreateAsync(exercise);
            return CreatedAtAction(nameof(GetById), new { id = exercise.ExerciseId }, exercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Exercise exercise)
        {
            if (exercise == null || id != exercise.ExerciseId)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(exercise);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exercise = await repository.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
