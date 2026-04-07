using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LabLinkBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentItemController : ControllerBase
    {
        private readonly IAppointmentItemService _appointmentItemService;
        public AppointmentItemController(IAppointmentItemService appointmentItemService)
        {   
            _appointmentItemService = appointmentItemService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(AppointmentItemDto appointmentItemDto)
        {
            if (appointmentItemDto == null )
            {
                return BadRequest(new { message = "Appointment item data is required." });
            }
            if(appointmentItemDto.PanelId == null && appointmentItemDto.TestId == null)
            {
                return BadRequest(new {message= "panel or test is needed"});
            }
            if(appointmentItemDto.PanelId != null && appointmentItemDto.TestId != null)
            {
                return BadRequest(new {message= "panel and test both not needed! request only one."});
            }

            try
            {
                var result = await _appointmentItemService.CreateAsync(appointmentItemDto);
                return Ok(new { message = "Appointment items created successfully.", data = result });
            }
            catch (Exception ex)    
            {
                return StatusCode(500, new { message = "Failed to process appointment item", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AppointmentItemUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest(new { message = "Appointment item data is required." });
            }
            try
            {
                var result = await _appointmentItemService.UpdateAsync(id, updateDto);
                return Ok(new { message = "Appointment item updated successfully.", data = result });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)    
            {
                return StatusCode(500, new { message = "Failed to update appointment item", detail = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _appointmentItemService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = $"Appointment item with ID {id} not found." });
                }
                return Ok(new { message = "Appointment item fetched successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to fetch appointment item", detail = ex.Message });
            }
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointmentId(int appointmentId)
        {
            try
            {
                var result = await _appointmentItemService.GetByAppointmentIdAsync(appointmentId);
                return Ok(new { message = "Appointment items fetched successfully.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to fetch appointment items", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _appointmentItemService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to delete appointment item", detail = ex.Message });
            }
        }
    }
}