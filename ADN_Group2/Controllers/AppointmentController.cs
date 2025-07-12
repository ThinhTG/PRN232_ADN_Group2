using Core.enums;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interface;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;
        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isHomeKit"> nếu là null thì sẽ lấy tất cả</param>
        /// <param name="status"> --: Lấy tất cả,  0: Pending,   1:WaitingToCollect,  2:InProgress,    3: Completed, 4: Collected </param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentReadDTO>>> 
            GetAll(bool? isHomeKit,AppointmentStatus? status)
        {
            var appointments = await _service.GetAllAsync(isHomeKit, status);
            return Ok(appointments);
        }
        [HttpGet("appointment-of-user")]
        public async Task<ActionResult<IEnumerable<AppointmentReadDTO>>> GetAppointmentByUserIdAsync(Guid userId)
        {
            var appointments = await _service.GetAppointmentByUserIdAsync(userId);
            return Ok(appointments);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentReadDTO>> GetById(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentReadDTO>> Create(AppointmentCreateUpdateDTO dto)
        {
            var created = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.AppointmentId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, AppointmentCreateUpdateDTO dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("need-collect")]
        public async Task<ActionResult<IEnumerable<AppointmentReadDTO>>> GetNeedCollectByUser(Guid userId, AppointmentStatus? status)
        {
            var all = await _service.GetAppointmentByUserIdAsync(userId);
            var filtered = all.Where(a => (status == null || a.Status == status.ToString()) && ( a.IsHomeKit == true)).ToList();
            return Ok(filtered);
        }
    }
} 