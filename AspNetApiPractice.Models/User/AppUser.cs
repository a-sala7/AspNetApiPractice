﻿using AspNetApiPractice.Models.Shop;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspNetApiPractice.Models.User
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
  
        [Required]
        public string LastName { get; set; }
        
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Product> Wishlist { get; set; }
    }
}
