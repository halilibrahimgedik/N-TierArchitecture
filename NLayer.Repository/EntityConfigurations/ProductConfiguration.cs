using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.EntityConfigurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id); // bunu tanımlamaya gerek yoktu, çünkü efCore bu isimlendirmeyi anlıyor.Anlamasaydı belirtmelydik (örn: Product_Id isimlendirmesini anlamaz)
            builder.Property(x => x.Id).UseIdentityColumn(); // id alanını otomatik artan yapar. Birşey belirtmezsek bir bir artar.

            builder.Property(x => x.Stock).IsRequired();

            builder.Property(x => x.Price).IsRequired()
                   .HasColumnType("decimal(18,2)"); // virgülden önce 16 karakter , virgülden sonra 2 karakter. Toplam = 18 karakter

            builder.Property(x => x.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            // EfCore Product ve Category tablosu arasındaki ilişkiyi anlıyor biliyor ve uyguluyor. Bunu yazmamıza gerek yok fakat görelim diye yazıyorum.
            // One to Many Relationship 
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p=>p.CategoryId);
        }
    }
}
