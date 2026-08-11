using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SkillGroup>> GetById(Guid id)
    {
        var group = await skillService.GetByIdAsync(id);
        return group is null ? NotFound() : Ok(group);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<SkillGroup>> Create(SkillGroupRequest request)
    {
        var group = await skillService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, SkillGroupRequest request)
    {
        await skillService.UpdateAsync(id, request);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await skillService.DeleteAsync(id);
        return NoContent();
    }
}
