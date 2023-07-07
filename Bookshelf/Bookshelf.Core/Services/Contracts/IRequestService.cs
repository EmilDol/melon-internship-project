﻿using Bookshelf.Core.DTOs;
using Bookshelf.Core.Models;
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

        Task<bool> CheckMotivation(Priority priority, string motivation);
    }
}
