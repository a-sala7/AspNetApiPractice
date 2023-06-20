namespace AspNetApiPractice.ViewModels.Shop;

public class WishListViewModel
{
    IEnumerable<ProductViewModel> Products { get; }

    public WishListViewModel(IEnumerable<ProductViewModel> products)
    {
        Products = products;
    }
}