using AspNetApiPractice.Data;
using AspNetApiPractice.Models.Shop;

namespace AspNetApiPractice.API.Utility;
public static class DbInitializer
{
    private static Product[] products = new Product[]
    {
        new()
        {
            Name_Ar = "اااالللل",
            Name_En = "Dummy",
            Description_Ar = "تفاصيل",
            Description_En = "Desc",
            Price = 129.99M
        },
        new()
        {
            Name_Ar = "اااالللل 2",
            Name_En = "Dummy 2",
            Description_Ar = "تفاصيل 2",
            Description_En = "Desc 2",
            Price = 15.99M
        },
        new()
        {
            Name_Ar = "اااالللل 3",
            Name_En = "Dummy 3",
            Description_Ar = "تفاصيل 3",
            Description_En = "Desc 3",
            Price = 21.99M
        }
    };

    private static Category[] categories = new Category[]
    {
        new Category()
        {
            Name_Ar = "صنف 1",
            Name_En = "Category 1",
            Products = new Product[] { products[0], products[1] }
        },
        new Category()
        {
            Name_Ar = "صنف 2",
            Name_En = "Category 2",
            Products = new Product[] { products[2] }
        }
    };
    public static void Initialize(WebApplication app) 
    {
        var dbContext = app
            .Services
            .CreateScope()
            .ServiceProvider
            .GetService<Entities>();

        if (dbContext.Products.Any() || dbContext.Categories.Any())
            return;

        dbContext.Categories.AddRange(categories);

        dbContext.SaveChanges();
    }
}