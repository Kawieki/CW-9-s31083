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
    public async Task<IActionResult> CreateNewPrescriptionAsync([FromBody] PrescriptionCreateDto prescription)
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
        catch (DateValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (MaxLimitReached e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"An unexpected error has occured {e.Message}");
        }
        
    }
}