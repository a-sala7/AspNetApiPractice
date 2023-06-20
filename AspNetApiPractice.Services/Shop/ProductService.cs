using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Data.UnitOfWork;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.Services.Helpers;
using AspNetApiPractice.ViewModels.Shop;

namespace AspNetApiPractice.Services.Shop;
public class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private static Func<Product, ProductViewModel> MappingExpression = p => new ProductViewModel {
        Id = p.Id,
        CategoryId = p.CategoryId,
        Price = p.Price
    };
    public ProductService(IRepository<Product> repository,
        IRepository<Category> categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = repository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductViewModel>> All()
    {
        var products = await _productRepository.All();
        return MapAndLocalize(products);
    }
    public async Task<IEnumerable<ProductViewModel>> GetByCategory(int categoryId)
    {
        var category = await _categoryRepository.All();
        if(category.Any(c => c.Id == categoryId) == false)
            throw new NotFoundException("Category", categoryId);
            
        var products = await _productRepository.All(c => c.CategoryId == categoryId);
        return MapAndLocalize(products);
    }
    
    private IEnumerable<ProductViewModel> MapAndLocalize(IEnumerable<Product> products)
    {
        var result = products.Select(MappingExpression).ToArray();

        LocalizationHelper.LocalizeProperties(
            src: products,
            target: result
        );

        return result;
    }

    public async Task<ProductViewModel> Add(CreateProductCommand command)
    {
        Category? category = await _categoryRepository.GetById(command.CategoryId);
        if(category is null){
            throw new NotFoundException("Category", command.CategoryId);
        }

        var p = new Product(){
            Name_Ar = command.Name_Ar,
            Name_En = command.Name_En,
            Description_Ar = command.Description_Ar,
            Description_En = command.Description_En,
            Price = command.Price,
            CategoryId = command.CategoryId
        };
        var newProd = _productRepository.Add(p);
        await _unitOfWork.SaveChangesAsync();
        var productViewModel = MappingExpression.Invoke(newProd);
        LocalizationHelper.LocalizeProperties(newProd, productViewModel);
        return productViewModel;
    }

    public async Task<ProductViewModel> Edit(EditProductCommand command)
    {
        Product? prodInDb = await _productRepository.GetById(command.Id);
        if(prodInDb is null)
            throw new NotFoundException("Product", command.Id);

        prodInDb.Name_Ar = command.Name_Ar;
        prodInDb.Name_En = command.Name_En;
        prodInDb.Description_Ar = command.Description_Ar;
        prodInDb.Description_En = command.Description_En;
        prodInDb.Price = command.Price;
        prodInDb.CategoryId = command.CategoryId;
        
        await _unitOfWork.SaveChangesAsync();
        var productViewModel = MappingExpression.Invoke(prodInDb);
        LocalizationHelper.LocalizeProperties(prodInDb, productViewModel);
        return productViewModel;
    }

    public async Task<ProductViewModel?> GetById(int id)
    {
        var prod = await _productRepository.GetById(id);
        if(prod is null)
            return null;
        
        var productViewModel = MappingExpression.Invoke(prod);
        LocalizationHelper.LocalizeProperties(prod, productViewModel);
        return productViewModel;
    }

    public async Task Delete(int id)
    {
        Product? product = await _productRepository.GetById(id);
        if(product is null)
            throw new NotFoundException("Product", id);

        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();
    }
}
