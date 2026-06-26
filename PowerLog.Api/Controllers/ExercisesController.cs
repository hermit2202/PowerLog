using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.DTOs.Exercise;
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
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
        {
            var exercises = await repository.GetAllAsync();

            var dtos = exercises.Select(e => new ExerciseDto
            {
                ExerciseId = e.ExerciseId,
                Name = e.Name,
                Type = e.Type,
                Description = e.Description,
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto>> GetById(Guid id)
        {
            var exercise = await repository.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            var dto = new ExerciseDto
            {
                ExerciseId = exercise.ExerciseId,
                Name = exercise.Name,
                Type = exercise.Type,
                Description = exercise.Description,
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDto>> Create(CreateExerciseDto dto)
        {
            var exercise = new Exercise
            {
                Name = dto.Name,
                Type = dto.Type,
                Description = dto.Description,
            };

            await repository.CreateAsync(exercise);

            var resultDto = new ExerciseDto
            {
                ExerciseId = exercise.ExerciseId,
                Name = exercise.Name,
                Type = exercise.Type,
                Description = exercise.Description,
            };

            return CreatedAtAction(nameof(GetById), new { id = resultDto.ExerciseId }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateExerciseDto dto)
        {
            var exercise = await repository.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }

            exercise.Name = dto.Name;
            exercise.Type = dto.Type;
            exercise.Description = dto.Description;

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
