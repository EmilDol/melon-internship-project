using Bookshelf.Core.DTOs.Categories;
using Bookshelf.Core.DTOs.Requests;
using Bookshelf.Core.DTOs.Resources;
using Bookshelf.Infrastructure;
using Bookshelf.Infrastructure.Models;
using Bookshelf.Infrastructure.Models.Enums;

using Microsoft.EntityFrameworkCore;

namespace Bookshelf.Core.Services
{
    public class ResourceService : Contracts.IResourceService
    {
        private readonly ApplicationDbContext _context;
        public ResourceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ResourceGetDTO>> GetAll()
        {
            var result = await _context.Resources
                .Select(r => new ResourceGetDTO
                {
                    Id = r.Id,
                    Title = r.Title,
                    Author = r.Author,
                    Type = r.Type.ToString(),
                    Status = r.Status.ToString(),
                })
                .ToListAsync();

            return result;
        }

        public async Task<ResourceDetailsDTO> GetDetails(int id)
        {
            var result = await _context.Resources
                .Where(r => r.Id == id)
                .Select(r => new ResourceDetailsDTO
                {
                    Id = r.Id,
                    Title = r.Title,
                    Author = r.Author,
                    Categories = r.Categories.Select(c => new CategoryDTO()).ToList(),
                    DateTaken = r.DateTaken.ToString(),
                    ExpectedReturnDate = r.ExpectedReturnDate.ToString(),
                    Type = r.Type.ToString(),
                    Status = r.Status.ToString(),
                    FilePath = r.FilePath
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<ResourceEditDTO> GetEdit(int id)
        {
            var result = await _context.Resources
                .Where(r => r.Id == id)
                .Select(r => new ResourceEditDTO
                {
                    Id = r.Id,
                    Title = r.Title,
                    Author = r.Author,
                    Categories = r.Categories.Select(c => new CategoryDTO()).ToList()
                })
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task Add(ResourceAddDTO resourceDTO)
        {
            var newResource = new Resource
            {
                Title = resourceDTO.Title,
                Author = resourceDTO.Author,
                //Add carteegorii
                Type = (ResourceType)Enum.Parse(typeof(ResourceType), resourceDTO.Type),
                FilePath = resourceDTO.FilePath
            };

            //if(resourceDTO.DateTaken != null )
            //{
            //    newResource.DateTaken = DateTime.Parse(resourceDTO.DateTaken);
            //}
            //if (resourceDTO.ExpectedReturnDate != null)
            //{
            //    newResource.ExpectedReturnDate = DateTime.Parse(resourceDTO.ExpectedReturnDate);
            //}
            //if (resourceDTO.DateTaken != null)
            //{
            //    newResource.DateTaken = DateTime.Parse(resourceDTO.DateTaken);
            //}
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

        public async Task Edit(ResourceEditDTO newResource)
        {
            var oldResource = await _context.Resources.FirstOrDefaultAsync(i => i.Id == newResource.Id);

            oldResource.Title = newResource.Title;
            oldResource.Author = newResource.Author;
            //oldResource.Categories = newResource.Categories;

            await _context.SaveChangesAsync();
        }

        public async Task<ResourceGetDTO> RequestToResource(RequestAddDTO request)
        {
            //var request = await
            //ResourceAddDto model = new ResourceAddViewModel();

            return null;
        }
    }
}
