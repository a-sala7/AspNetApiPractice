using AspNetApiPractice.API.Config;
using AspNetApiPractice.API.Filters;
using AspNetApiPractice.API.Utility;
using AspNetApiPractice.Data;
using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Models;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Models.User;
using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.Services.User;
using AspNetApiPractice.ViewModels.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddSerilogLogging();

builder.Services.ConfigureControllersWithModelValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Entities>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetValue<string>("DefaultConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<Entities>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IRepository<BaseModel>, BaseModelRepository>();

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();


var app = builder.Build();

DbInitializer.Initialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
