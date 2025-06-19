using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _service;
        public BlogController(IBlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Blog>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> Create(Blog entity)
        {
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.BlogId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Blog entity)
        {
            if (id != entity.BlogId) return BadRequest();
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 