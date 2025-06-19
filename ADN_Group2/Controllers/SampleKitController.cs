using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleKitController : ControllerBase
    {
        private readonly ISampleKitService _service;
        public SampleKitController(ISampleKitService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<SampleKit>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<SampleKit>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<SampleKit>> Create(SampleKit entity)
        {
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SampleKit entity)
        {
            if (id != entity.Id) return BadRequest();
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