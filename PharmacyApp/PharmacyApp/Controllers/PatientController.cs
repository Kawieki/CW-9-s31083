using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Services;

namespace PharmacyApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IDbService service): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPatientsAsync([FromRoute] int id)
    {
        return Ok("ok");

    }
}