using System.ComponentModel.Design;

using Bookshelf.Core.DTOs;
using Bookshelf.Core.Services.Contracts;
using Bookshelf.Infrastructure;
using Bookshelf.Infrastructure.Models;
using Bookshelf.Infrastructure.Models.Enums;

using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Core.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ApplicationDbContext _context;
        public ResourceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResourceDTO>> GetAll()
        {
            var result = await _context.Resources
                .Select(r => new ResourceDTO
                {
                    Title = r.Title,
                    Author = r.Author,
                    Categories = r.Categories,
                    DateTaken = r.DateTaken.ToString(),
                    ExpectedReturnDate = r.ExpectedReturnDate.ToString(),
                    Type = r.Type.ToString(),
                    Status = r.Status.ToString(),
                    FilePath = r.FilePath
                })
                .ToListAsync();

            return result;
        }

        public async Task<ResourceDTO> GetById(int id)
        {
            var result = await _context.Resources
                .Select(s => new ResourceDTO
                {
                    Title = s.Title,
                    Author = s.Author,
                    Categories = s.Categories,
                    DateTaken = s.DateTaken.ToString(),
                    ExpectedReturnDate = s.ExpectedReturnDate.ToString(),
                    Type = s.Type.ToString(),
                    Status = s.Status.ToString(),
                    FilePath = s.FilePath
                })
                .FirstOrDefaultAsync(r => r.Id == id);

            return result;
        }

        public async Task Add(ResourceDTO resourceDTO)
        {
            var newResource = new Resource
            {
                Title = resourceDTO.Title,
                Author = resourceDTO.Author,
                Categories = resourceDTO.Categories,
                Type = (ResourceType)Enum.Parse(typeof(ResourceType), resourceDTO.Type),
                FilePath = resourceDTO.FilePath
            };

            if(resourceDTO.DateTaken != null )
            {
                newResource.DateTaken = DateTime.Parse(resourceDTO.DateTaken);
            }
            if (resourceDTO.ExpectedReturnDate != null)
            {
                newResource.ExpectedReturnDate = DateTime.Parse(resourceDTO.ExpectedReturnDate);
            }
            if (resourceDTO.DateTaken != null)
            {
                newResource.DateTaken = DateTime.Parse(resourceDTO.DateTaken);
            }
            if (resourceDTO.Status != null)
            {
                newResource.Status = (ResourceStatus)Enum.Parse(typeof(ResourceStatus), resourceDTO.Status);
            }

            await _context.Resources.AddAsync(newResource);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var resource = await _context.Resources.FirstOrDefaultAsync(i => i.Id == id);

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(ResourceDTO newResource)
        {
            var oldResource = await _context.Resources.FirstOrDefaultAsync(i => i.Id == newResource.Id);

            oldResource.Title = newResource.Title;
            oldResource.Author = newResource.Author;
            oldResource.Categories = newResource.Categories;
            oldResource.Type = (ResourceType)Enum.Parse(typeof(ResourceType), newResource.Type);
            oldResource.FilePath = newResource.FilePath;
            oldResource.DateTaken = DateTime.Parse(newResource.DateTaken);
            oldResource.ExpectedReturnDate = DateTime.Parse(newResource.ExpectedReturnDate);
            oldResource.Status = (ResourceStatus)Enum.Parse(typeof(ResourceStatus), newResource.Status);

            await _context.SaveChangesAsync();
        }

        //public async Task<ResourceDTO> RequestToResource(RequestAddDTO request)
        //{
        //    var request = await
        //    ResourceAddDto model = new ResourceAddViewModel();
        //}
    }
}
