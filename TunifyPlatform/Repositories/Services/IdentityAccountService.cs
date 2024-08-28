using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TunifyPlatform.Models;
using TunifyPlatform.Models.DTO;
using TunifyPlatform.Repositories.interfaces;

namespace TunifyPlatform.Repositories.Services
{
    public class IdentityAccountService : IAccount
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        // Inject jwt service 
        private JwtTokenService _jwtTokenService;

        public IdentityAccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        // Register Method
        public async Task<UserDto> Register(RegisterUserDto registerUserDto, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (result.Succeeded)
            {

                // add roles to new registed user
                await _userManager.AddToRolesAsync(user, registerUserDto.Roles);

                return new UserDto()
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await _jwtTokenService.GenerateToken(user, System.TimeSpan.FromMinutes(10)),
                    Roles = await _userManager.GetRolesAsync(user),
                };
            }

            foreach (var error in result.Errors)
            {
                var errorCode = error.Code.Contains("Password") ? nameof(registerUserDto) :
                                error.Code.Contains("Email") ? nameof(registerUserDto) :
                                error.Code.Contains("Username") ? nameof(registerUserDto) : "";

                modelState.AddModelError(errorCode, error.Description);
            }

            return null;
        }

        // Login Method
        public async Task<UserDto> UserAuthentication(LoginDto userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.Username);

            if (user != null)
            {
                bool passValidation = await _userManager.CheckPasswordAsync(user, userLogin.Password);

                if (passValidation)
                {
                    return new UserDto()
                    {
                        Id = user.Id,
                        Username = user.UserName,
                        Token = await _jwtTokenService.GenerateToken(user, System.TimeSpan.FromMinutes(10)),
                    };
                }
            }

            return null;
        }

        // Logout Method
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
