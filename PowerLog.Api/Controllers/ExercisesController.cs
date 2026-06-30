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
        private readonly IExerciseService exerciseService;

        public ExercisesController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll()
        {
            try
            {
                var dtos = await exerciseService.GetAllExerciseAsync();
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка при получении упражнений: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto>> GetById(Guid id)
        {
            try
            {
                var dto = await exerciseService.GetExerciseByIdAsync(id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDto>> Create(CreateExerciseDto dto)
        {
            try
            {
                var exercise = await exerciseService.CreateExerciseAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = exercise.ExerciseId }, exercise);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Ошибка при создании упражнения: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateExerciseDto dto)
        {
            try
            {
                var exercise = await exerciseService.UpdateExerciseAsync(dto, id);
                return Ok(exercise);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await exerciseService.DeleteExerciseAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
