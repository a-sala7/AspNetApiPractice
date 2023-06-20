using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Models;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.Services.User;

namespace AspNetApiPractice.API.Config;

public static class RepositoryAndServiceConfig
{
    public static void AddRepositoriesAndServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Product>, Repository<Product>>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IRepository<BaseModel>, BaseModelRepository>();
    }
}