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
    public class KitController : ControllerBase
    {
        private readonly IKitService _service;
        public KitController(IKitService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KitReadDTO>>> GetAll()
        {
            var kits = await _service.GetAllAsync();
            return Ok(kits);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<KitReadDTO>> GetById(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<KitReadDTO>> Create(KitCreateUpdateDTO dto)
        {
            var created = await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.KitId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, KitCreateUpdateDTO dto)
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
    }
} 