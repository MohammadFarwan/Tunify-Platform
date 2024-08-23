using Microsoft.AspNetCore.Mvc.ModelBinding;
using TunifyPlatform.Models.DTO;

namespace TunifyPlatform.Repositories.interfaces
{
    public interface IAccount
    {
        // Add Register
        public Task<UserDto> Register(RegisterUserDto registerUserDto, ModelStateDictionary modelState);

        // Add Login
        public Task<UserDto> UserAuthentication(LoginDto userLogin);

        // Add Logout
        public Task Logout();

    }
}
