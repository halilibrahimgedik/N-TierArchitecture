using Microsoft.EntityFrameworkCore;
using NLayer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        // DbContextOptions nesnesi ile vt bağlantı cümleciğini program.cs den alıcağımızı bildirmiştik yukarıda. Bu yüzden bu metoda gerek yok artık
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Bağlantı yolu");
        //}

        // Best Practice -> Entity Configurasyonlarını IEntityTypeConfigurations<> arayüzü ile yapmalıyız. yönetilebilir olmadığından ve karmaşaya sebeb olduğundan bu metotda yapmamalıyız
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
