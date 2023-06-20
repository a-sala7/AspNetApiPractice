using AspNetApiPractice.API.Filters;

namespace AspNetApiPractice.API.Config;

public static class GlobalExceptionHandlingConfig
{
    public static void AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddScoped<GlobalExceptionHandlerMiddleware>();
    }
    public static void UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}