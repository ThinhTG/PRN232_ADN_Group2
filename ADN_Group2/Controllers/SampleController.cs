using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _service;
        public SampleController(ISampleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Sample>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Sample>> GetById(System.Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<Sample>> Create(Sample entity)
        {
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.SampleId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(System.Guid id, Sample entity)
        {
            if (id != entity.SampleId) return BadRequest();
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