using CareerOS.Server.Models;
using CareerOS.Server.Models.Requests;
using CareerOS.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CareerOS.Server.Controllers;

// Authorize is applied at the class level, not per-action: unlike the other
// controllers, the diary has no public endpoint at all, so every action
// needs it.
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DiaryController(IDiaryService diaryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DiaryEntry>>> Get(
        [FromQuery] string? search,
        [FromQuery] string? category,
        [FromQuery] string? tag)
    {
        return Ok(await diaryService.SearchAsync(search, category, tag));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DiaryEntry>> GetById(Guid id)
    {
        var entry = await diaryService.GetByIdAsync(id);
        return entry is null ? NotFound() : Ok(entry);
    }

    [HttpPost]
    public async Task<ActionResult<DiaryEntry>> Create(DiaryEntryRequest request)
    {
        var entry = await diaryService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = entry.Id }, entry);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, DiaryEntryRequest request)
    {
        await diaryService.UpdateAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await diaryService.DeleteAsync(id);
        return NoContent();
    }
}
