using Bookshelf.Core.DTOs.Categories;
using Bookshelf.Core.Services.Contracts;
using Bookshelf.Infrastructure;
using Bookshelf.Infrastructure.Models;

using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            var result = await _context.Categories
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return result;
        }

        public async Task Add(CategoryDTO category)
        {
            var newCategory = new Category
            {
                Name = category.Name
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
