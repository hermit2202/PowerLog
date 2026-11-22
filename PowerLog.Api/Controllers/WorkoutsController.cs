using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerLog.Api.Extensions;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.DTOs.Workout;

namespace PowerLog.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            this.workoutService = workoutService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetAll(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var workouts = await workoutService.GetAllAsync(userId, cancellationToken);
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var workout = await workoutService.GetByIdAsync(id, userId, cancellationToken);
            return Ok(workout);

        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDto>> Create(CreateWorkoutDto dto, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var workout = await workoutService.CreateAsync(dto, userId, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = workout.WorkoutId }, workout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateWorkoutDto dto, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var workout = await workoutService.UpdateAsync(dto, id, userId, cancellationToken);
            return Ok(workout);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();
            var success = await workoutService.DeleteAsync(id, userId, cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
