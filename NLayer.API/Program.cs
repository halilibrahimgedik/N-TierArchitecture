using Autofac.Extensions.DependencyInjection;
using Autofac;
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
using NLayer.API.Modules;

var builder = WebApplication.CreateBuilder(args);

//! T�m Controller'lar�m�za �zel Tan�mlad���m�z ValidateFilterAttribute adl� Attribute'�m�z� Ekleyelim.
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()));

//! Default D�nen ModelState Hata Filter'�n� Bast�ral�m
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//! Caching
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),option =>
    {
        // Migrationlar� API katman�m�zda olu�turmamak i�in (Repository Katman�nda migrationlar� olu�turmal�y�z) bu ayar� programa s�ylemeliyiz. Bak benim AppDbContext S�n�f�m API Katman�nda de�il Repository Katman�nda demeliyiz.
        // Bu kodda Assembly s�n�f� �zerinden GetAssembly() metodu ile "AppDbContext" olarak belirtti�imiz s�n�f�n bulundu�u Assembly (proje) ismini ".GetName().Name" diyerek al diyoruz.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); 

        /*option.MigrationsAssembly("NLayer.Repository");*/  // tip g�vensiz �al��ma. Yar�n proje katman ad� de�i�irse hata al�r�z. Yukar�daki gibi tan�mlamal�y�z.
    });
});


//! NotFoundFilter 
builder.Services.AddScoped(typeof(NotFoundFilter<>));
//! AutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile));


//! AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));


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
