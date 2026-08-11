using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectEntry>> GetById(Guid id)
    {
        var entry = await projectService.GetByIdAsync(id);
        return entry is null ? NotFound() : Ok(entry);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ProjectEntry>> Create(ProjectEntryRequest request)
    {
        var entry = await projectService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = entry.Id }, entry);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProjectEntryRequest request)
    {
        await projectService.UpdateAsync(id, request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await projectService.DeleteAsync(id);
        return NoContent();
    }
}
