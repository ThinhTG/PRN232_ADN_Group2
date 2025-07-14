using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interface;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestResultController : ControllerBase
    {
        private readonly ITestResultService _service;
        public TestResultController(ITestResultService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<TestResultReadDTO>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TestResultReadDTO>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [HttpGet("by-appointment/{appointmentId}")]
        public async Task<ActionResult<IEnumerable<TestResultReadDTO>>> GetByAppointmentId(Guid appointmentId)
        {
            var results = await _service.GetByAppointmentIdAsync(appointmentId);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<TestResultReadDTO>> Create(TestResultCreateUpdateDTO entity)
        {
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.ResultId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TestResultCreateUpdateDTO entity)
        {
            await _service.UpdateAsync(id,entity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 