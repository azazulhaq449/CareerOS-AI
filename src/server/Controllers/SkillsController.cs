using CareerOS.Server.Models;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController(ISkillService skillService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SkillGroup>>> Get()
    {
        return Ok(await skillService.GetAllAsync());
    }
}
