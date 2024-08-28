using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TunifyPlatform.Models.DTO;
using TunifyPlatform.Repositories.interfaces;

namespace TunifyPlatform.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount userService;

        public AccountController(IAccount _context)
        {
            userService = _context;
        }

        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerdUserDto)
        {
            var user = await userService.Register(registerdUserDto, this.ModelState);

            if (ModelState.IsValid)
            {
                return user;
            }

            if (user == null)
            {
                return Unauthorized();
            }

            return BadRequest();
        }



        // Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userService.UserAuthentication(loginDto);

            if (user == null)
            {
                return Unauthorized();
            }

            return user;
        }

        // Logout
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();
            return Ok();
        }
    }
}
