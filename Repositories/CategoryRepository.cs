using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data;
using ReservationSystem.Models;
using ReservationSystem.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return;

            var productIds = await _context.Products
                .Where(p => p.CategoryId == id)
                .Select(p => (int?)p.Id)
                .ToListAsync();

            var orderItems = await _context.OrderItems
                .Where(oi => productIds.Contains(oi.ProductId))
                .ToListAsync();

            if (orderItems.Any())
                _context.OrderItems.RemoveRange(orderItems);

            var products = await _context.Products
                .Where(p => p.CategoryId == id)
                .ToListAsync();

            if (products.Any())
                _context.Products.RemoveRange(products);

            _context.Categories.Remove(category);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
