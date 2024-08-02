using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Legislation.Api.User;

[ApiController]
[Route("api/[controller]")]
public class UsersController(ILogger<UsersController> logger): ControllerBase
{
    [HttpGet("login")]
    [AllowAnonymous]
    public IResult Login([FromQuery] string username)
    {
        var claimsPrincipal = new ClaimsPrincipal(
          new ClaimsIdentity(
            [new Claim(ClaimTypes.Name, username), new Claim(ClaimTypes.Role, "legislator")],
            BearerTokenDefaults.AuthenticationScheme
          )
        );
        return Results.SignIn(claimsPrincipal);
    }

    [HttpGet("user")]
    [Authorize(Roles = "legislator")]
    public IActionResult GetUser()
    {
        var userName = User?.Identity?.Name?.Replace("\"", string.Empty);
        var role = User?.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();
        logger.LogInformation("Hello from {Name} role of {Role}", userName, role);
        return Ok(new { Name = userName, Role = role });
    }
}
