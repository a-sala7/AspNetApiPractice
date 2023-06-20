namespace AspNetApiPractice.Services.User;

public interface IUserIdProvider
{
    string GetCurrentUserId();
}