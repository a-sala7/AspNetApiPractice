using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Data.Repository.Shop;
using AspNetApiPractice.Models;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.Services.User;

namespace AspNetApiPractice.API.Config;

public static class RepositoryAndServiceConfig
{
    public static void AddRepositoriesAndServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Product>, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IRepository<Category>, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IUserService, UserService>();
    }
}