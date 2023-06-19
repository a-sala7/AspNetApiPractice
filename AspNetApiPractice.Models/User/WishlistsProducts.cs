using AspNetApiPractice.Models.Shop;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Models.User
{
    [Table(name: "WishlistsProducts")]
    [PrimaryKey(nameof(UserId), nameof(ProductId))]
    public class WishlistsProducts
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
