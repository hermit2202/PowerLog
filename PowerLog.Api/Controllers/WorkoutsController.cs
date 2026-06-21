using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IRepository<Workout> repository;

        public WorkoutsController(IRepository<Workout> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workout>>> GetAll()
        {
            var workouts = await repository.GetAllAsync();
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetById(Guid id)
        {
            var workout = await repository.GetByIdAsync(id);
            if (workout == null)
            {
                return NotFound();
            }
            return Ok(workout);
        }

        [HttpPost]
        public async Task<ActionResult<Workout>> Create(Workout workout)
        {
            await repository.CreateAsync(workout);
            return CreatedAtAction(nameof(GetById), new { id = workout.WorkoutId }, workout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Workout workout)
        {
            if (workout == null || id != workout.WorkoutId)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(workout);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var workout = await repository.GetByIdAsync(id);
            if (workout == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
