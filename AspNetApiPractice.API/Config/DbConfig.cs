using AspNetApiPractice.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetApiPractice.API.Config;

public static class DbConfig
{
    public static void AddDb(this IServiceCollection services, string? dbConnectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(dbConnectionString);
        services.AddDbContext<Entities>(opt =>
        {
            opt.UseSqlServer(dbConnectionString);
        });
    }    
}