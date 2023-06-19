using AspNetApiPractice.API.Utility;
using AspNetApiPractice.Data;
using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Models;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Models.User;
using AspNetApiPractice.Services.Shop;
using AspNetApiPractice.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Entities>(opt =>
{
    opt.UseSqlServer("Data Source=ASRock\\SQLEXPRESS;Initial Catalog=ApiPractice;Integrated Security=True;TrustServerCertificate=True;");
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

app.MapControllers();

app.Run();
