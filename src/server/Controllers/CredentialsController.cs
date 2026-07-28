using CareerOS.Server.Models;
using CareerOS.Server.Services;
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
}
