using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Repository.Entity;
using Service.DTOs;
using Service.Interface;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentReadDTO>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentReadDTO>> GetById(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return entity;
        }
        [HttpGet("payments-of-users/{userId}")]
        public async Task<IEnumerable<PaymentReadDTO>> GetPaymentByUserIdAsync(Guid userId)
        {
            var entity = await _service.GetPaymentByUserIdAsync(userId);
            return entity;
        }

        [HttpPost("create-link")]
        public async Task<ActionResult<CreatePaymentResult>> Create(PaymentResponse model)
        {
            try
            {
                var created = await _service.CreateLinkAsync(model);
                return Ok(created);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = "An error occurred while creating the payment link.", details = ex.Message });

            }
        }
        [HttpPost("check-payment")]
        public async Task<ActionResult> CheckPaymentAsync(string orderCode)
        {
            try
            {
                await _service.CheckStatusAsync(orderCode);
                return Ok(new { message = "Payment status checked successfully." });
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = "An error occurred while creating the payment link.", details = ex.Message });
            }
            
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