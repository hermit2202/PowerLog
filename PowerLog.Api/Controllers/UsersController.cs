using Microsoft.AspNetCore.Mvc;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> repository;

        public UsersController(IRepository<User> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await repository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            await repository.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, User user)
        {
            if (user == null || id != user.UserId)
            {
                return BadRequest();
            }
            await repository.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
