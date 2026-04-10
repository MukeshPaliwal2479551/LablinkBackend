using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabLinkBackend.DTO;
using LabLinkBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabLinkBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService testService;
        public TestController(ITestService _testService)
        {
            testService=_testService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await testService.Create(dto);
                return Ok("Test Created Successfully");
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                await testService.Deactivate(id);
                return Ok("Test deactivated successfully");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Test not found");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateTestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await testService.Update(id, dto);
                return Ok("Test updated successfully");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Test not found");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var test = await testService.GetById(id);
            if (test == null) return NotFound("Test not found");
            return Ok(test);
        }
         [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? name, [FromQuery] string? code)
        {
            var tests = await testService.GetTests(name, code);
            return Ok(tests);
        }
    }
}