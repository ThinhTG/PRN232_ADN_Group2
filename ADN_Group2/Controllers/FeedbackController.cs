using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _service;
        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackReadDTO>>> GetAll()
        {
            var feedbacks = await _service.GetAllAsync();
            return Ok(feedbacks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackReadDTO>> GetById(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<FeedbackReadDTO>> Create(FeedbackCreateUpdateDTO dto)
        {
            try
            {
                var created = await _service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.FeedbackId }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating feedback." });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, FeedbackCreateUpdateDTO dto)
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

        [HttpGet("service/{serviceId}")]
        public async Task<ActionResult<IEnumerable<FeedbackReadDTO>>> GetByServiceId(Guid serviceId)
        {
            var feedbacks = await _service.GetByServiceIdAsync(serviceId);
            return Ok(feedbacks);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<FeedbackReadDTO>>> GetByUserId(Guid userId)
        {
            var feedbacks = await _service.GetByUserIdAsync(userId);
            return Ok(feedbacks);
        }
    }
} 