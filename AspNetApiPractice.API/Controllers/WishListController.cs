using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.Services.User;
using AspNetApiPractice.ViewModels.Shop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetApiPractice.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WishListController : ControllerBase
{
    private readonly IWishListService _wishListService;
    private readonly IUserIdProvider _useridprovider;

    public WishListController(IWishListService wishListService,
        IUserIdProvider useridprovider)
    {
        _wishListService = wishListService;
        _useridprovider = useridprovider;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _wishListService.GetWishList(_useridprovider.GetCurrentUserId()));
    }

    [HttpPost("{productId}")]
    public async Task<IActionResult> Add(int productId)
    {
        await _wishListService.AddProduct(_useridprovider.GetCurrentUserId(), productId);
        return Ok();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Delete(int productId)
    {
        await _wishListService.RemoveProduct(_useridprovider.GetCurrentUserId(), productId);
        return Ok();
    }
}