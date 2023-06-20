using AspNetApiPractice.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace AspNetApiPractice.API.Config;

public static class ControllersAndValidationConfig
{
    public static void ConfigureControllersWithModelValidation(this IServiceCollection services)
    {
        services.AddControllers(opt => {
            opt.Filters.Add<ValidationFilterAttribute>();
        });
        services.Configure<ApiBehaviorOptions>(opt => {
            opt.SuppressModelStateInvalidFilter = true;
        });
    }
}