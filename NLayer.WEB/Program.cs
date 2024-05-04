using Autofac.Extensions.DependencyInjection;
using Autofac;
using NLayer.Web.Modules;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayer.Repository;
using NLayer.Service.Mapping;
using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using FluentValidation;
using NLayer.WEB.Filters;
using Autofac.Core;
using NLayer.WEB.Services;
using System.Net.Http.Headers;
using System.Xml;
using NLayer.Core.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ! AppDbContext Settings
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"), option =>
    {
        // Migrationlarý API katmanýmýzda oluþturmamak için (Repository Katmanýnda migrationlarý oluþturmalýyýz) bu ayarý programa söylemeliyiz. Bak benim AppDbContext Sýnýfým API Katmanýnda deðil Repository Katmanýnda demeliyiz.
        // Bu kodda Assembly sýnýfý üzerinden GetAssembly() metodu ile "AppDbContext" olarak belirttiðimiz sýnýfýn bulunduðu Assembly (proje) ismini ".GetName().Name" diyerek al diyoruz.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

        /*option.MigrationsAssembly("NLayer.Repository");*/  // tip güvensiz çalýþma. Yarýn proje katman adý deðiþirse hata alýrýz. Yukarýdaki gibi tanýmlamalýyýz.
    });
});

// ! FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters().AddValidatorsFromAssemblyContaining<ProductDtoValidator>();

// ! AutoMapper
builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MapProfile)));
//builder.Services.AddAutoMapper(typeof(WebMvcMappProfile));

// ! Filters
builder.Services.AddScoped(typeof(NotFoundFilter<>));

//! AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new ServiceModule()));

builder.Services.AddHttpClient<ProductApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

builder.Services.AddHttpClient<CategoryApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});


builder.Services.AddHttpClient();
builder.Services.AddScoped(typeof(IGenericApiService<,>), typeof(GenericApiService<,>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
