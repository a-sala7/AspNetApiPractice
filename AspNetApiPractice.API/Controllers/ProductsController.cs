using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AspNetApiPractice.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? categoryId)
    {
        if(categoryId is null)
            return Ok(await _productService.All());

        if((await _categoryService.All()).Any(c => c.Id == categoryId) == false)
            return NotFound(
                ResponseViewModel<object>.NotFound("category", categoryId.Value)
            );

        return Ok(await _productService.GetByCategory(categoryId.Value));
    }
}