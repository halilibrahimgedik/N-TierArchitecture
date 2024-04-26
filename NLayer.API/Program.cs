using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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
