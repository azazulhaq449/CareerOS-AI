using CareerOS.Server.Models;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IProjectService projectService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProjectEntry>>> Get()
    {
        return Ok(await projectService.GetAllAsync());
    }
}
