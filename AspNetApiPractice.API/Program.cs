using AspNetApiPractice.API.Config;
using AspNetApiPractice.API.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddSerilogLogging();
builder.Services.ConfigureControllersWithModelValidation();
builder.Services.ConfigureSwagger();
builder.Services.AddDb(builder.Configuration.GetValue<string>("DefaultConnection"));
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.AddRepositoriesAndServices();
builder.Services.AddGlobalExceptionHandler();


var app = builder.Build();

DbInitializer.Initialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalExceptionHandler();

app.MapControllers();

app.Run();
