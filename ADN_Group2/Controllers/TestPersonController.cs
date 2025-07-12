using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using Service.DTOs;
using Service.Interface;
using System.Collections.Generic;

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
        public async Task<IEnumerable<TestPersonReadDTO>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TestPersonReadDTO>> GetById(System.Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<List<TestPersonReadDTO>>> Create(List<TestPersonCreateUpdateDTO> entity)
        {
            var created = await _service.AddAsync(entity);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(System.Guid id, TestPersonCreateUpdateDTO entity)
        {
            await _service.UpdateAsync(id,entity);
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