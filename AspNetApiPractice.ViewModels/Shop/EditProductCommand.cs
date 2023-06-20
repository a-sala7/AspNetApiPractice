using System.ComponentModel.DataAnnotations;

namespace AspNetApiPractice.ViewModels.Shop;
public class EditProductCommand
{
    public int Id { get; set; }
    [Required]
    public string Name_Ar { get; set; }
    
    [Required]
    public string Name_En { get; set; }

    public string? Description_Ar { get; set; }
    public string? Description_En { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
