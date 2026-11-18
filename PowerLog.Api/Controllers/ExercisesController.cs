using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var userId = GetCurrentUserId();
            var dtos = await exerciseService.GetAllAsync(userId, cancellationToken);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExerciseDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            try
            {
                var dto = await exerciseService.GetByIdAsync(id, userId, cancellationToken);
                return Ok(dto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ExerciseDto>> Create(CreateExerciseDto dto, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            try
            {
                var exercise = await exerciseService.CreateAsync(dto, userId, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { id = exercise.ExerciseId }, exercise);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateExerciseDto dto, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            try
            {
                var exercise = await exerciseService.UpdateAsync(id, dto, userId, cancellationToken);
                return Ok(exercise);
            }
            catch (InvalidOperationException ex) { return Conflict(ex.Message); }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (UnauthorizedAccessException ex) { return StatusCode(403, ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var userId = GetCurrentUserId();
            try
            {
                await exerciseService.DeleteAsync(id, userId, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(ex.Message); }
            catch (UnauthorizedAccessException ex) { return StatusCode(403, ex.Message); }
        }

        private Guid GetCurrentUserId()
        {
            return Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new InvalidOperationException("User ID not found in token.")
            );
        }
    }
}
