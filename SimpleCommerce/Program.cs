using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleCommerce.Infrastructure;
using SimpleCommerce.Services;
using SimpleCommerce.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new SimpleCommerceAutoMapperProfiles());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var infrastructureSettings = builder.Configuration.GetSection("Infrastructure").Get<InfrastructureSettings>();

builder.Services.AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<ICartRepository, CartRepository>();


if (string.IsNullOrEmpty(infrastructureSettings.ConnectionString))
{
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseInMemoryDatabase("Database");
    });
}
else
{
    builder.Services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(infrastructureSettings.ConnectionString);
    });
}

var app = builder.Build();
if (string.IsNullOrEmpty(infrastructureSettings.ConnectionString) == false)
{
    try
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            db.Database.Migrate();
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
   
}


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
