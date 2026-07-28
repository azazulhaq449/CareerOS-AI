using CareerOS.Server.Models;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperienceController(IExperienceService experienceService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExperienceEntry>>> Get()
    {
        return Ok(await experienceService.GetAllAsync());
    }
}
