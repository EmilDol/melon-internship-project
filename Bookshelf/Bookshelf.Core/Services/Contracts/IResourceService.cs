using Bookshelf.Core.DTOs;

namespace Bookshelf.Core.Services.Contracts
{
    public interface IResourceService
    {
        Task<List<ResourceDTO>> GetAll();

        Task<ResourceDTO> GetById(int id); 

        Task Add(ResourceDTO resourceDTO);

        Task Delete(int id);

        Task Edit(ResourceDTO resource);
        
        Task<ResourceDTO> RequestToResource(RequestAddDTO request);
    }
}
