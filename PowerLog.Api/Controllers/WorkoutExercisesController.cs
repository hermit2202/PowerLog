using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.Set;
using PowerLog.Core.DTOs.WorkoutExercise;
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
        public async Task<ActionResult<IEnumerable<WorkoutExerciseDto>>> GetAll()
        {
            var workoutExercises = await repository.GetAllAsync();

            var dtos = workoutExercises.Select(w => new WorkoutExerciseDto
            {
                ExerciseId = w.ExerciseId,
                WorkoutExerciseId = w.WorkoutExerciseId,
                WorkoutId = w.WorkoutId,
                Order = w.Order,
                Sets = w.Sets?.Select(s => new SetDto
                {
                    SetId = s.SetId,
                    Weight = s.Weight,
                    Reps = s.Reps,
                    RPE = s.RPE,
                }).ToList() ?? new List<SetDto>(),
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutExerciseDto>> GetById(Guid id)
        {
            var workoutExercise = await repository.GetByIdAsync(id);
            if (workoutExercise == null)
            {
                return NotFound();
            }

            var dto = new WorkoutExerciseDto
            {
                ExerciseId = workoutExercise.ExerciseId,
                WorkoutExerciseId = workoutExercise.WorkoutExerciseId,
                WorkoutId = workoutExercise.WorkoutId,
                Order = workoutExercise.Order,
                Sets = workoutExercise.Sets?.Select(s => new SetDto
                {
                    SetId = s.SetId,
                    Weight = s.Weight,
                    Reps = s.Reps,
                    RPE = s.RPE,
                }).ToList() ?? new List<SetDto>(),
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutExerciseDto>> Create(CreateWorkoutExerciseDto dto)
        {
            var workoutExercise = new WorkoutExercise
            {
                WorkoutId = dto.WorkoutId,
                Order = dto.Order,
                Sets = dto.Sets.Select(s => new Set
                {
                    Weight = s.Weight,
                    Reps = s.Reps,
                    RPE = s.RPE,
                }).ToList()
            };

            await repository.CreateAsync(workoutExercise);

            var resultDto = new WorkoutExerciseDto
            {
                ExerciseId = workoutExercise.ExerciseId,
                WorkoutExerciseId = workoutExercise.WorkoutExerciseId,
                WorkoutId = workoutExercise.WorkoutId,
                Order = workoutExercise.Order,
                Sets = workoutExercise.Sets.Select(s => new SetDto
                {
                    SetId = s.SetId,
                    Weight = s.Weight,
                    Reps = s.Reps,
                    RPE = s.RPE,
                }).ToList(),
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.WorkoutExerciseId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateWorkoutExerciseDto dto)
        {
            var workoutExercise = await repository.GetByIdAsync(id);
            if (workoutExercise == null || id != workoutExercise.WorkoutExerciseId)
            {
                return BadRequest();
            }

            workoutExercise.WorkoutId = dto.WorkoutId;
            workoutExercise.Order = dto.Order;
            workoutExercise.Sets = dto.Sets.Select(s => new Set
            {
                SetId = s.SetId,
                Weight = s.Weight,
                Reps = s.Reps,
                RPE = s.RPE,
            }).ToList();

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
