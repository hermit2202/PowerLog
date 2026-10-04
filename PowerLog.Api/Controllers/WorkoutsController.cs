using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var userId = Guid.Parse(
                    User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("User ID not found in token.")
                );

                var workouts = await workoutService.GetAllAsync(userId, cancellationToken);
                return Ok(workouts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении тренировок: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var workout = await workoutService.GetByIdAsync(id, cancellationToken);
                return Ok(workout);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDto>> Create(CreateWorkoutDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.Parse(
                    User.FindFirstValue(ClaimTypes.NameIdentifier)
                    ?? throw new InvalidOperationException("User ID not found in token.")
                );

                var workout = await workoutService.CreateAsync(dto, userId, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = workout.WorkoutId }, workout);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ошибка при создании тренировки: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateWorkoutDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var workout = await workoutService.UpdateAsync(dto, id, cancellationToken);
                return Ok(workout);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Ошибка при обновлении тренировки: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var success = await workoutService.DeleteAsync(id, cancellationToken);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
