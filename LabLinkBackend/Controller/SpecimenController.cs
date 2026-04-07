using LabLinkBackend.DTO;
using LabLinkBackend.Models;
using LabLinkBackend.Services;
using FluentValidation;
using LabLinkBackend.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LabLinkBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecimenController : ControllerBase
    {
        private readonly ISpecimenService _specimenService;
        private readonly IValidator<SpecimenCreateDTO> _validator;

        public SpecimenController(ISpecimenService specimenService)
        {
            _specimenService = specimenService;
            _validator = new SpecimenCreateDTOValidator();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecimen([FromBody] SpecimenCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid specimen data.");

            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var specimen = await _specimenService.CreateSpecimenAsync(dto);
            return Ok(specimen);
        }
            [HttpDelete]
            [Route("delete/{id}")]
            public async Task<IActionResult> DeleteSpecimen(int id)
            {
                var result = await _specimenService.DeleteSpecimenAsync(id);
                if (!result)
                    return NotFound($"Specimen with ID {id} not found.");
                return NoContent();
            }
    }
}