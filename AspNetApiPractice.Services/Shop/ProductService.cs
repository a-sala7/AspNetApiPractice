using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Helpers;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Services.Helpers;
using AspNetApiPractice.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetApiPractice.Services.Shop
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        public ProductService(IRepository<Product> repository)
        {
            _productRepository = repository;
        }
        public async Task<IEnumerable<ProductViewModel>> All()
        {
            var products = await _productRepository.All();
            return MapAndLocalize(products);
        }
        public async Task<IEnumerable<ProductViewModel>> GetByCategory(int categoryId)
        {
            var products = await _productRepository.All(c => c.CategoryId == categoryId);
            return MapAndLocalize(products);
        }

        private IEnumerable<ProductViewModel> MapAndLocalize(IEnumerable<Product> products)
        {
            var result = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Price = p.Price,
            }).ToArray();

            LocalizationHelper.LocalizeProperties(
                src: products,
                target: result,
                HttpRequestHelper.GetHeaderValue("lang")
            );

            return result;
        }
    }
}
