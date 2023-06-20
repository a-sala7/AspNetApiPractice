using System.Security.Claims;
using AspNetApiPractice.Services.User;

namespace AspNetApiPractice.API.Utility;

public class UserIdProvider : IUserIdProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserId()
    {
        return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}