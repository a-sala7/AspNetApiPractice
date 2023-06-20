using System.ComponentModel.DataAnnotations;

namespace AspNetApiPractice.ViewModels.User;
public class CreateUserCommand
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(200)]
    public string Address { get; set; }
}
