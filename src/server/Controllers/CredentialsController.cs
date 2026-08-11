using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CredentialsController(ICredentialsService credentialsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CredentialsResponse>> Get()
    {
        return Ok(await credentialsService.GetCredentialsAsync());
    }

    [Authorize]
    [HttpPost("certifications")]
    public async Task<ActionResult<Certification>> CreateCertification(CertificationRequest request)
    {
        var certification = await credentialsService.CreateCertificationAsync(request);
        return CreatedAtAction(nameof(Get), new { }, certification);
    }

    [Authorize]
    [HttpPut("certifications/{id:guid}")]
    public async Task<IActionResult> UpdateCertification(Guid id, CertificationRequest request)
    {
        await credentialsService.UpdateCertificationAsync(id, request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("certifications/{id:guid}")]
    public async Task<IActionResult> DeleteCertification(Guid id)
    {
        await credentialsService.DeleteCertificationAsync(id);
        return NoContent();
    }

    [Authorize]
    [HttpPut("education")]
    public async Task<IActionResult> UpdateEducation(EducationEntryRequest request)
    {
        await credentialsService.UpdateEducationAsync(request);
        return NoContent();
    }
}
