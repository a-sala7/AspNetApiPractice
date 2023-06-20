namespace AspNetApiPractice.ViewModels.Shop;

public class WishListViewModel
{
    public IEnumerable<ProductViewModel> Products { get; set; }

    public WishListViewModel(IEnumerable<ProductViewModel> products)
    {
        Products = products;
    }
}