using AspNetApiPractice.Data;
using AspNetApiPractice.Data.Repository;
using AspNetApiPractice.Models;
using AspNetApiPractice.Models.Shop;
using AspNetApiPractice.Services.Shop;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Entities>(opt =>
{
    opt.UseSqlServer("Data Source=ASalah-LT-11134\\SQLEXPRESS;Initial Catalog=ApiPractice;Integrated Security=True;TrustServerCertificate=True;");
});

builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IRepository<BaseModel>, BaseModelRepository>();

var app = builder.Build();

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
