using AspNetApiPractice.ViewModels.User;

namespace AspNetApiPractice.Services.User
{
    public interface IUserService
    {
        Task CreateUser(CreateUserCommand command);
    }
}