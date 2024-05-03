using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "Faber Castell Grip",
                Price = 100,
                Stock = 200,
                CreatedDate = DateTime.Now


            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                Name = "Faber Castell Econ",
                Price = 79,
                Stock = 334,
                CreatedDate = DateTime.Now


            },
             new Product
             {
                 Id = 3,
                 CategoryId = 1,
                 Name = "Rotring 500",
                 Price = 600,
                 Stock = 60,
                 CreatedDate = DateTime.Now


             },
               new Product
               {
                   Id = 4,
                   CategoryId = 2,
                   Name = "Atomik Alışkanlıklar",
                   Price = 600,
                   Stock = 60,
                   CreatedDate = DateTime.Now


               },
               new Product
               {
                   Id = 5,
                   CategoryId = 2,
                   Name = "Tutunamayanlar",
                   Price = 459,
                   Stock = 320,
                   CreatedDate = DateTime.Now


               });
        }
    }
}
