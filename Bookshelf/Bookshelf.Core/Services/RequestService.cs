using Bookshelf.Core.DTOs;
using Bookshelf.Core.Models;
using Bookshelf.Core.Services.Contracts;
using Bookshelf.Infrastructure;
using Bookshelf.Infrastructure.Models.Enums;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bookshelf.Core.Services
{
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext _context;

        public RequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RequestGetDTO>> GetAllUnapproved()
        {
            var requests = await _context.Requests
                .Where(r => r.Status == RequestStatus.AwaitingConfirmation)
                .Select(u => new RequestGetDTO
                {
                    Id = u.Id,
                    Author = u.Author,
                    Priority = u.Priority.ToString(),
                    Status = u.Status.ToString(),
                    Title = u.Title
                })
                .ToListAsync();

            return requests;
        }

        public async Task<List<RequestGetDTO>> GetAllApproved()
        {
            var requests = await _context.Requests
                .Where(r => r.Status == RequestStatus.InPreparation || r.Status == RequestStatus.Delivering)
                .Select(u => new RequestGetDTO
                {
                    Id = u.Id,
                    Author = u.Author,
                    Priority = u.Priority.ToString(),
                    Status = u.Status.ToString(),
                    Title = u.Title
                })
                .ToListAsync();

            return requests;
        }

        public async Task<List<RequestGetDTO>> GetAllDiscarded()
        {
            var requests = await _context.Requests
                .Where(r => r.Status == RequestStatus.Discarded)
                .Select(u => new RequestGetDTO
                {
                    Id = u.Id,
                    Author = u.Author,
                    Priority = u.Priority.ToString(),
                    Status = u.Status.ToString(),
                    Title = u.Title
                })
                .ToListAsync();

            return requests;
        }

        public async Task<RequestDetailsDTO> GetDetails(int requestId, string userId)
        {
            var requests = await _context.Requests
                .Include(c => c.Followers)
                .Include(b => b.Upvoters)
                .Select(u => new RequestDetailsDTO
                {
                    Id = u.Id,
                    Author = u.Author,
                    DateAdded = u.DateAdded.ToString("dd MMMM yyyy"),
                    Link = u.Link,
                    Motivation = u.Motivation,
                    Priority = u.Priority.ToString(),
                    Status = u.Status.ToString(),
                    Title = u.Title,
                    Upvotes = u.Upvotes,
                    Upvoted = u.Upvoters
                        .Select(s => s.UserId)
                        .Contains(userId),
                    Followed = u.Followers
                        .Select(v => v.UserId)
                        .Contains(userId)
                })
                .FirstOrDefaultAsync();

            return requests;
        }

        public async Task Approve(int requestId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
            {
                return;
            }

            request.Status = RequestStatus.InPreparation;
            _context.SaveChanges();
        }

        public async Task Reject(int requestId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
            {
                return;
            }

            request.Status = RequestStatus.Discarded;
            _context.SaveChanges();
        }

        public async Task<RequestStatus> StatusUpdate(int requestId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
            request.Status++;
            await _context.SaveChangesAsync();

            return request.Status;
        }
    }
}
