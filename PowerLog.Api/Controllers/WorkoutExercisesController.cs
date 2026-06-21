using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutExercisesController : ControllerBase
    {
        private readonly IRepository<WorkoutExercise> repository;

        public WorkoutExercisesController(IRepository<WorkoutExercise> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutExercise>>> GetAll()
        {
            var workoutExercises = await repository.GetAllAsync();
            return Ok(workoutExercises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExercise>> GetById(Guid id)
        {
            var workoutExercise = await repository.GetByIdAsync(id);
            if (workoutExercise == null)
            {
                return NotFound();
            }
            return Ok(workoutExercise);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutExercise>> Create(WorkoutExercise workoutExercise)
        {
            await repository.CreateAsync(workoutExercise);
            return CreatedAtAction(nameof(GetById), new { id = workoutExercise.WorkoutExerciseId }, workoutExercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, WorkoutExercise workoutExercise)
        {
            if (workoutExercise == null || id != workoutExercise.WorkoutExerciseId)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(workoutExercise);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var workoutExercise = await repository.GetByIdAsync(id);
            if (workoutExercise == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
