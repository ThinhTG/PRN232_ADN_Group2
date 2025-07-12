using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interface;

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
        public async Task<IEnumerable<SampleReadDTO>> GetAll() => await _service.GetAllAsync();
        /// <summary>
        /// Lấy danh sách mẫu theo AppointmentIds
        /// </summary>
        /// <param name="appoinmentId"></param>
        /// <returns></returns>
        [HttpGet("by-appointment")]
        public async Task<IEnumerable<SampleReadDTO>> GetByAppointment(Guid appoinmentId) 
            => await _service.GetSampleByAppointmentIdAsync(appoinmentId);

        [HttpGet("{id}")]
        public async Task<ActionResult<SampleReadDTO>> GetById(System.Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }

        [HttpPost]
        public async Task<ActionResult<SampleReadDTO>> Create(SampleCreateUpdateDTO entity)
        {
            var created = await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.SampleId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(System.Guid id, SampleCreateUpdateDTO entity)
        {
            await _service.UpdateAsync(id,entity);
            return Ok("update successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(System.Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

		/// <summary>
		/// User lấy mẫu ( nhập ApointmentId và danh sách SampleIds lấy mẫu)
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost("collect")]
        public async Task<IActionResult> CollectSamples([FromBody] SampleCollectDTO dto)
        {
            await _service.CollectSamplesAsync(dto);
            return Ok("Samples collected");
        }

		/// <summary>
		/// Staff xác nhận đã nhận mẫu ( nhập SampleIds và ngày nhận mẫu )
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost("receive")]
        public async Task<IActionResult> ReceiveSamples([FromBody] SampleReceiveDTO dto)
        {
            await _service.ReceiveSamplesAsync(dto);
            return Ok("Samples received.");
        }
        /// <summary>
		/// User lấy mẫu ( nhập ApointmentId và danh sách SampleIds lấy mẫu)
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPost("onsite-collect")]
        public async Task<IActionResult> CollectOnsiteSamples([FromBody] SampleCollectDTO dto)
        {
            await _service.CollectOnsiteSamplesAsync(dto);
            return Ok("Samples collected");
        }
    }
} 