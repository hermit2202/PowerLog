using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.Exercise;
using PowerLog.Core.DTOs.Set;
using PowerLog.Core.DTOs.Workout;
using PowerLog.Core.DTOs.WorkoutExercise;
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
        public async Task<ActionResult<IEnumerable<WorkoutDto>>> GetAll()
        {
            var workouts = await repository.GetAllAsync();

            var dtos = workouts.Select(w => new WorkoutDto
            {
                WorkoutId = w.WorkoutId,
                UserId = w.UserId,
                Planned = w.Planned,
                Actual = w.Actual,
                Exercises = w.WorkoutExercises?.Select(e => new WorkoutExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    WorkoutExerciseId = e.WorkoutExerciseId,
                    WorkoutId = e.WorkoutId,
                    Order = e.Order,
                    Sets = e.Sets?.Select(s => new SetDto
                    {
                        SetId = s.SetId,
                        Weight = s.Weight,
                        Reps = s.Reps,
                        RPE = s.RPE,
                    }).ToList() ?? new List<SetDto>(),
                }).ToList() ?? new List<WorkoutExerciseDto>(),
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDto>> GetById(Guid id)
        {
            var workout = await repository.GetByIdAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            var dto = new WorkoutDto
            {
                WorkoutId = workout.WorkoutId,
                UserId = workout.UserId,
                Planned = workout.Planned,
                Actual = workout.Actual,
                Exercises = workout.WorkoutExercises?.Select(e => new WorkoutExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    WorkoutExerciseId = e.WorkoutExerciseId,
                    WorkoutId = e.WorkoutId,
                    Order = e.Order,
                    Sets = e.Sets?.Select(s => new SetDto
                    {
                        SetId = s.SetId,
                        Weight = s.Weight,
                        Reps = s.Reps,
                        RPE = s.RPE,
                    }).ToList() ?? new List<SetDto>(),
                }).ToList() ?? new List<WorkoutExerciseDto>(),
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutDto>> Create(CreateWorkoutDto dto)
        {
            var workout = new Workout
            {
                UserId = dto.UserId,
                Planned = dto.Planned,
                Actual = dto.Actual,
            };

            await repository.CreateAsync(workout);

            var resultDto = new WorkoutDto
            {
                WorkoutId = workout.WorkoutId,
                UserId = workout.UserId,
                Planned = workout.Planned,
                Actual = workout.Actual,
                Exercises = new List<WorkoutExerciseDto>(),
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.WorkoutId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateWorkoutDto dto)
        {
            var workout = await repository.GetByIdAsync(id);

            if (workout == null || id != workout.WorkoutId)
            {
                return BadRequest();
            }

            workout.Planned = dto.Planned;
            workout.Actual = dto.Actual;

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
