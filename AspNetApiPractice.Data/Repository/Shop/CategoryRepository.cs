using AspNetApiPractice.Models.Shop;

namespace AspNetApiPractice.Data.Repository.Shop;

public class CategoryRepository : BaseModelRepository<Category>
{
    public CategoryRepository(Entities dbContext) : base(dbContext)
    {
    }
}