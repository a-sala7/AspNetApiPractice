using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Helpers;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Services.Helpers;
using AspNetApiPractice.ViewModels.Shop;

namespace AspNetApiPractice.Services.Shop;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _repository;

    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryViewModel>> All()
    {
        var categories = await _repository.All();

        var result = categories.Select(c => new CategoryViewModel(){
            Id = c.Id
        }).ToArray();

        LocalizationHelper.LocalizeProperties(
            src: categories, 
            target: result
        );

        return result;
    }
}