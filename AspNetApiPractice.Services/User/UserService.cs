using AspNetApiPractice.Models.User;
using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.ViewModels.Shared;
using AspNetApiPractice.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace AspNetApiPractice.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateUser(CreateUserCommand command)
        {
            var user = new AppUser()
            {
                Email = command.Email,
                UserName = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Address = command.Address
            };

            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded)
            {
                var errorsModel = new ValidationErrors(result.Errors.Select(e => e.Description));
                throw new AppException(errorsModel);
            }
        }
    }
}
