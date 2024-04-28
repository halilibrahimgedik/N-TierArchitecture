using Microsoft.EntityFrameworkCore;
using NLayer.Core.Model;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        { 
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            //Eager Loading -> Datayı Çekerken Yanında Kategorileride getirdik
            return await _dbContext.Products.Include(p => p.Category)
                                            .ToListAsync();
        }
    }
}
