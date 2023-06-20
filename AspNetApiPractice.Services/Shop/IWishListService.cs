using AspNetApiPractice.ViewModels.Shop;

namespace AspNetApiPractice.Services.Shop;

public interface IWishListService
{
    Task<WishListViewModel> GetWishList(string userId);
    Task AddProduct(string userId, int productId);
    Task RemoveProduct(string userId, int productId);
}