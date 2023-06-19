using AspNetApiPractice.Models.Shop;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspNetApiPractice.Models.User
{
    public class AppUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
  
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        public virtual ICollection<Product> Wishlist { get; set; }
    }
}
