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

        Task<List<RequestGetDTO>> GetFollowedById(string id);

        Task<RequestDetailsDTO> GetDetails(int requestId, string userId);

        Task StatusUpdate(int requestId);

        Task<RequestStatus> GetStatus(int requestId);

        Task Approve(int requestId);

        Task Reject(int requestId);

        Task<bool> Exists(string requestName);

        Task Add(RequestAddDTO model);

        Task Edit(int id, string status);

        Task<bool> CheckMotivation(string priority, string motivation);

        Task Follow(string userId, int requestId);

        Task Upvote(string userId, int requestId);

        Task<List<string>> GetFollowers(int id);
    }
}
