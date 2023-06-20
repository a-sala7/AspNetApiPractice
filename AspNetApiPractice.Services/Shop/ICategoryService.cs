using AspNetApiPractice.ViewModels.Shop;

namespace AspNetApiPractice.Services.Shop;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> All();
}