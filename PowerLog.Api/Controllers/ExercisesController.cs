using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PowerLog.Api.Extensions;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.DTOs.Exercise;

namespace PowerLog.Api.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetAll(CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var dtos = await exerciseService.GetAllAsync(userId, cancellationToken);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var dto = await exerciseService.GetByIdAsync(id, userId, cancellationToken);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDto>> Create(CreateExerciseDto dto, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var exercise = await exerciseService.CreateAsync(dto, userId, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = exercise.ExerciseId }, exercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateExerciseDto dto, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            var exercise = await exerciseService.UpdateAsync(id, dto, userId, cancellationToken);
            return Ok(exercise);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var userId = User.GetUserId();

            await exerciseService.DeleteAsync(id, userId, cancellationToken);
            return NoContent();
        }
    }
}
