using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Exceptions;
using PharmacyApp.Services;

namespace PharmacyApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController(IDbService service): ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientsAsync([FromRoute] int id)
    {
        if (id < 1) return BadRequest("Id must be greater than 0");
        try
        {
            var patientDetails = await service.GetPatientDetailsAsync(id);
            return Ok(patientDetails);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}