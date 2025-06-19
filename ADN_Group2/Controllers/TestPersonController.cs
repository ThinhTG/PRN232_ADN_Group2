using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestPersonController : ControllerBase
    {
        private readonly ITestPersonService _service;
        public TestPersonController(ITestPersonService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<TestPerson>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TestPerson>> GetById(System.Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<TestPerson>> Create(TestPerson entity)
        {
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.PersonId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(System.Guid id, TestPerson entity)
        {
            if (id != entity.PersonId) return BadRequest();
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