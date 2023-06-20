using AspNetApiPractice.API.Utility;
using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Data.Repository.Shop;
using AspNetApiPractice.Data.UnitOfWork;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Models.User;
using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.Services.User;

namespace AspNetApiPractice.API.Config;

public static class RepositoryAndServiceConfig
{
    public static void AddRepositoriesAndServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IRepository<Product>, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IRepository<Category>, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IRepository<WishlistsProducts>, Repository<WishlistsProducts>>();
        services.AddScoped<IWishListService, WishListService>();

        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IUserIdProvider, UserIdProvider>();
    }
}