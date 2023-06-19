using AspNetApiPractice.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Models.Shop
{
    public class Category : BaseModel, IBilingual
    {
        [Required]
        [MaxLength(50)]
        public required string Name_Ar { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Name_En { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
