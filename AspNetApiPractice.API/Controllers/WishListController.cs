using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.Services.User;
using AspNetApiPractice.ViewModels.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetApiPractice.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WishListController : ControllerBase
{
    private readonly IWishListService _wishListService;
    private readonly IUserIdProvider _userIdProvider;

    public WishListController(IWishListService wishListService,
        IUserIdProvider useridprovider)
    {
        _wishListService = wishListService;
        _userIdProvider = useridprovider;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        WishListViewModel wishlist = await _wishListService.GetWishList(_userIdProvider.GetCurrentUserId());
        return Ok(wishlist);
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> Add(int productId)
    {
        await _wishListService.AddProduct(_userIdProvider.GetCurrentUserId(), productId);
        return Ok();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        await _wishListService.RemoveProduct(_userIdProvider.GetCurrentUserId(), productId);
        return Ok();
    }
}