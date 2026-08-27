using BasicAuthApi.Infrastructures.Errors;
using BasicAuthApi.Infrastructures.Interfaces;
using BasicAuthApi.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuthApi.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController(IRegisterService registerService, ILoginService loginService) : ControllerBase
    {
        private readonly IRegisterService registerService = registerService;

        private readonly ILoginService loginService = loginService;

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterDto dto)
        {
            try
            {
                await registerService.RegisterAsync(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUsingEmailDto dto)
        {
            try
            {
                string token = await loginService.LoginUsingEmailAsync(dto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<UserDetailsDto> GetSignedUser()
        {
            try
            {
                string username = User.Claims.First(c => c.Type == "username").Value,
                       firstName = User.Claims.First(c => c.Type == "firstName").Value,
                       lastName = User.Claims.First(c => c.Type == "lastName").Value,
                       email = User.Claims.First(c => c.Type == "email").Value;
                UserDetailsDto dto = new(username, firstName, lastName, email);
                return dto is null ? NotFound() : Ok(dto);
            }
            catch (InternalServerException ex)
            {
                return Problem(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
