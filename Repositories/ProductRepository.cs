using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;
using ReservationSystem.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }


        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public List<Product> GetNewProductsArrivals(int count = 8)
        {
            return _context.Products
                           .OrderByDescending(p => p.DateAdded)
                           .Take(count)
                           .ToList();
        }

        public List<Product> GetPopularProducts(int count = 10)
        {
            return _context.Products
                           .Where(p => p.SalesCount > 0)
                           .OrderByDescending(p => p.SalesCount)
                           .Take(count)
                           .ToList();
        }
    }
}
