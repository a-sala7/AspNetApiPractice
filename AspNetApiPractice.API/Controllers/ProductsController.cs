using AspNetApiPractice.Services.Exceptions;
using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.ViewModels.Shop;
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

        return Ok(await _productService.GetByCategory(categoryId.Value));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        ProductViewModel? prod = await _productService.GetById(id);
        if(prod is null){
            throw new NotFoundException("Product", id);
        }

        return Ok(prod);
    }

    [HttpPost]
    public async Task <IActionResult> Create(CreateProductCommand command)
    {
        var addedProduct = await _productService.Add(command);
        return CreatedAtAction("GetById", new { id = addedProduct.Id }, addedProduct);
    }

    [HttpPut]
    public async Task<IActionResult> Edit(EditProductCommand command)
    {
        return Ok(await _productService.Edit(command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.Delete(id);
        return Ok();
    }
}