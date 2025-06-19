using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KitController : ControllerBase
    {
        private readonly IKitService _service;
        public KitController(IKitService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Kit>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Kit>> GetById(System.Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<Kit>> Create(Kit entity)
        {
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.KitId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(System.Guid id, Kit entity)
        {
            if (id != entity.KitId) return BadRequest();
            await _service.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(System.Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 