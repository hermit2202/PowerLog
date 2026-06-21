using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetsController : ControllerBase
    {
        private readonly IRepository<Set> repository;

        public SetsController(IRepository<Set> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Set>>> GetAll()
        {
            var sets = await repository.GetAllAsync();
            return Ok(sets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Set>> GetById(Guid id)
        {
            var set = await repository.GetByIdAsync(id);
            if (set == null)
            {
                return NotFound();
            }
            return Ok(set);
        }

        [HttpPost]
        public async Task<ActionResult<Set>> Create(Set set)
        {
            await repository.CreateAsync(set);
            return CreatedAtAction(nameof(GetById), new { id = set.SetId }, set);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Set set)
        {
            if (set == null || id != set.SetId)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(set);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var set = await repository.GetByIdAsync(id);
            if (set == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
