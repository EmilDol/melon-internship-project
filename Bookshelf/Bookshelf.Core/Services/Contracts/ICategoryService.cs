using Bookshelf.Core.DTOs;

namespace Bookshelf.Core.Services.Contracts
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAll();

        Task Add(CategoryDTO category);

        Task Delete(int id);
    }
}
