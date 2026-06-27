using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.Workout;
using PowerLog.Core.Interfaces;

namespace PowerLog.Api.Controllers
{
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
        public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetAll()
        {
            try
            {
                var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                var workouts = await workoutService.GetAllAsync(userId);
                return Ok(workouts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении тренировок: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDto>> GetById(Guid id)
        {
            try
            {
                var workout = await workoutService.GetByIdAsync(id);
                return Ok(workout);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDto>> Create(CreateWorkoutDto dto)
        {
            try
            {
                var userId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                var workout = await workoutService.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetById), new { id = workout.WorkoutId }, workout);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ошибка при создании тренировки: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateWorkoutDto dto)
        {
            try
            {
                var workout = await workoutService.UpdateAsync(dto, id);
                return Ok(workout);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Ошибка при обнавлении тренировки: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await workoutService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
