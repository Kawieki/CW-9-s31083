using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.DTOs;
using PharmacyApp.Exceptions;
using PharmacyApp.Services;

namespace PharmacyApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController(IDbService service): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateNewPrescription([FromBody] PrescriptionCreateDto prescription)
    {
        try
        {
            var result = await service.CreateNewPrescriptionAsync(prescription);
            return Ok(result);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}