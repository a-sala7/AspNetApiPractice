using AspNetApiPractice.Models.Interfaces;
using AspNetApiPractice.Models.User;
using System.ComponentModel.DataAnnotations;

namespace AspNetApiPractice.Models.Shop
{
    public class Product : BaseModel, IBilingual, ILocalizable
    {
        [Required]
        [MaxLength(200)]
        public string Name_Ar { get; set; }
        
            [Required]
        [MaxLength(200)]
        public string Name_En { get; set; }
        
        [MaxLength(3000)]
        public string? Description_Ar { get; set; }
        
        [MaxLength(3000)]
        public string? Description_En { get; set; }

        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<AppUser> Users { get; set; }
    }
}
