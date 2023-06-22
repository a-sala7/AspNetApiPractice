namespace AspNetApiPractice.ViewModels.User;

public class LoginResponse
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public string Id { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}