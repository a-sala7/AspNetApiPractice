using AspNetApiPractice.Data;
using AspNetApiPractice.Models.User;
using Microsoft.AspNetCore.Identity;

namespace AspNetApiPractice.API.Config;

public static class IdentityConfig
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<Entities>()
            .AddDefaultTokenProviders();
    }
}