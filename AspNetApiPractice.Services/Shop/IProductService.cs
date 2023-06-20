using AspNetApiPractice.ViewModels;
using AspNetApiPractice.ViewModels.Shop;

namespace AspNetApiPractice.Services.Shop;
public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> All();
    Task<IEnumerable<ProductViewModel>> GetByCategory(int categoryId);
    Task<ProductViewModel?> GetById(int id);
    Task<ProductViewModel> Add(CreateProductCommand command);
}
