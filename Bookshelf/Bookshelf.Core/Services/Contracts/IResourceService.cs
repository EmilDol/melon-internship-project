﻿using Bookshelf.Core.DTOs.Requests;
using Bookshelf.Core.DTOs.Resources;

namespace Bookshelf.Core.Services.Contracts
{
    public interface IResourceService
    {
        Task<List<ResourceGetDTO>> GetAll();

        Task<List<ResourceGetDTO>> GetTakenById(string id);

        Task<ResourceDetailsDTO> GetDetails(int id);

        Task<ResourceEditDTO> GetEdit(int id);

        Task Add(ResourceAddDTO resourceDTO);

        Task Delete(int id);

        Task Edit(ResourceEditDTO resource);
        
        Task<ResourceGetDTO> RequestToResource(RequestAddDTO request);
    }
}
