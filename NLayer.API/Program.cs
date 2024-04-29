using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Tüm Controller'larýmýza Özel Tanýmladýðýmýz ValidateFilterAttribute adlý Attribute'ümüzü Ekleyelim.
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()));

// Default Dönen ModelState Hata Filter'ýný Bastýralým
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),option =>
    {
        // Migrationlarý API katmanýmýzda oluþturmamak için (Repository Katmanýnda migrationlarý oluþturmalýyýz) bu ayarý programa söylemeliyiz. Bak benim AppDbContext Sýnýfým API Katmanýnda deðil Repository Katmanýnda demeliyiz.
        // Bu kodda Assembly sýnýfý üzerinden GetAssembly() metodu ile "AppDbContext" olarak belirttiðimiz sýnýfýn bulunduðu Assembly (proje) ismini ".GetName().Name" diyerek al diyoruz.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); 

        /*option.MigrationsAssembly("NLayer.Repository");*/  // tip güvensiz çalýþma. Yarýn proje katman adý deðiþirse hata alýrýz. Yukarýdaki gibi tanýmlamalýyýz.
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException(); // Custom Global Exception Middleware'imiz

app.UseAuthorization();

app.MapControllers();

app.Run();
