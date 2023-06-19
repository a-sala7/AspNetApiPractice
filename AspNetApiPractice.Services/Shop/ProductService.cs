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
            var result = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Price = p.Price,
            });
            for(int i = 0; i < products.Count(); i++)
            {
                LocalizationHelper.LocalizeProperties(
                    src: products.ElementAt(i), 
                    target: result.ElementAt(i), 
                    HttpRequestHelper.GetHeaderValue("lang")
                );
            }
            return result;
        }
    }
}
