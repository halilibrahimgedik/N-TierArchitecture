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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.Id).UseIdentityColumn();

            builder.Property(x=> x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category);
        }
    }
}
