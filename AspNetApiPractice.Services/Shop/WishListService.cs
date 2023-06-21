using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Data.UnitOfWork;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Models.User;
using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.Services.Helpers;
using AspNetApiPractice.ViewModels.Shop;

namespace AspNetApiPractice.Services.Shop;

public class WishListService : IWishListService
{
    private readonly IRepository<WishlistsProducts> _wishlistsProductsRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private static Func<Product, ProductViewModel> MappingExpression = p => new ProductViewModel {
        Id = p.Id,
        CategoryId = p.CategoryId,
        Price = p.Price
    };

    public WishListService(IRepository<WishlistsProducts> wishlistsProductsRepository,
        IRepository<Product> productRepository,
        IUnitOfWork unitOfWork)
    {
        _wishlistsProductsRepository = wishlistsProductsRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddProduct(string userId, int productId)
    {
        bool alreadyInWishList = (await _wishlistsProductsRepository
                .All(x => x.UserId == userId && x.ProductId == productId)
            ).Any();

        if(alreadyInWishList)
            throw new AppException("Product already in wishlist");

        _wishlistsProductsRepository.Add(new(){
            ProductId = productId,
            UserId = userId
        });

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<WishListViewModel> GetWishList(string userId)
    {
        IEnumerable<int> prodIds = (await _wishlistsProductsRepository.All(x => x.UserId == userId)).Select(x => x.ProductId);
        IEnumerable<Product> products = await _productRepository.All(p => prodIds.Contains(p.Id));
        ProductViewModel[] result = products.Select(MappingExpression).ToArray();

        LocalizationHelper.LocalizeProperties(
            src: products,
            target: result
        );

        return new WishListViewModel(result);
    }

    public async Task RemoveProduct(string userId, int productId)
    {
        WishlistsProducts? itemToDelete = (await _wishlistsProductsRepository
                .All(x => x.UserId == userId && x.ProductId == productId)
            ).FirstOrDefault();

        if(itemToDelete is null)
            throw new AppException("Product not in wishlist");

        _wishlistsProductsRepository.Delete(itemToDelete);
        await _unitOfWork.SaveChangesAsync();
    }
}