using AspNetApiPractice.Models.Shop;

namespace AspNetApiPractice.Data.Repository.Shop;

public class ProductRepository : BaseModelRepository<Product>
{
    public ProductRepository(Entities dbContext) : base(dbContext)
    {
    }
}