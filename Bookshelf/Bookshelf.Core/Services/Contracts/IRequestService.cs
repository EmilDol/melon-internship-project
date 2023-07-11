using Bookshelf.Core.DTOs.Categories;
using Bookshelf.Core.DTOs.Requests;
using Bookshelf.Infrastructure.Models.Enums;

namespace Bookshelf.Core.Services.Contracts
{
    public interface IRequestService
    {
        Task<List<RequestGetDTO>> GetAllUnapproved();

        Task<List<RequestGetDTO>> GetAllApproved();

        Task<List<RequestGetDTO>> GetAllDiscarded();

        Task<RequestDetailsDTO> GetDetails(int requestId, string userId);

        Task<RequestStatus> StatusUpdate(int requestId);

        Task Approve(int requestId);

        Task Reject(int requestId);

        Task<bool> Exists(string requestName);

        Task Add(RequestAddDTO model);

        Task Edit(int id, string status);

        Task<bool> CheckMotivation(string priority, string motivation);

        Task<List<CategoryDTO>> GetCategories();
    }
}
