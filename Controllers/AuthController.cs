using BasicAuthApi.Configurations;
using BasicAuthApi.Infrastructures;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthApi.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController(IRegisterService registerService, ILoginService loginService, IUserService userService) : ControllerBase
{
    private readonly IRegisterService registerService = registerService;

    private readonly ILoginService loginService = loginService;

    private readonly IUserService userService = userService;

    private async Task<IActionResult> LoginAsync(LoginUsingEmailDto? emailDto = null, LoginUsingUsernameDto? usernameDto = null)
    {
        string token;
        if (emailDto is not null)
        {
            token = await loginService.LoginUsingEmailAsync(emailDto);
        }
        else if (usernameDto is not null)
        {
            token = await loginService.LoginUsingUsernameAsync(usernameDto);
        }
        else
        {
            throw new NotImplementedException();
        }
        return Ok(new { token });
    }

    /// <summary>
    /// Enables a new user to be registered in the system.
    /// </summary>
    /// <param name="dto">Data to be send to the server.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterDto dto)
    {
        string token = await registerService.RegisterAsync(dto);

        return Ok(new { token });
    }

    /// <summary>
    /// Enables the user to login if he or she is registered in the system.
    /// </summary>
    /// <param name="dto">User's data containing email and password.</param>
    /// <returns>Some of the user's data serialized info Jwt if he or she is registered before.</returns>
    [HttpPost("login-with-email")]
    public async Task<IActionResult> LoginUsingEmailAsync(LoginUsingEmailDto dto)
    {
        return await LoginAsync(emailDto: dto);
    }

    /// <summary>
    /// Enables the user to login if he or she is registered in the system.
    /// </summary>
    /// <param name="dto">User's data containing username and password.</param>
    /// <returns>Some of the user's data serialized info Jwt if he or she is registered before.</returns>
    [HttpPost("login-with-username")]
    public async Task<IActionResult> LoginUsingUsernameAsync(LoginUsingUsernameDto dto)
    {
        return await LoginAsync(usernameDto: dto);
    }

    [HttpGet("me")]
    [Authorize(Roles = "User")]
    public ActionResult<UserDetailsDto> GetSignedInUser()
    {
        UserDetailsDto dto = User.MapClaimsToUserDetails();
        return Ok(dto);
    }

    [HttpGet("all")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetAllUsersAsync()
    {
        return Ok(await userService.GetAllAsync());
    }

    [HttpPost("add-role/{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> AddRoleToUser(string id, string role)
    {
        await userService.AddRoleToUserAsync(id, role);
        return Ok();
    }
}
