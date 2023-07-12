using Bookshelf.Core.DTOs.Requests;
using Bookshelf.Core.Services.Contracts;
using Bookshelf.Infrastructure;
using Bookshelf.Infrastructure.Models;
using Bookshelf.Infrastructure.Models.Enums;

using Microsoft.EntityFrameworkCore;

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
                .Include(i => i.Categories)
                .ThenInclude(ti => ti.Category)
                .Where(r => r.Status == RequestStatus.AwaitingConfirmation)
                .Select(u => new RequestGetDTO
                {
                    Id = u.Id,
                    Author = u.Author,
                    Priority = u.Priority.ToString(),
                    Status = u.Status.ToString(),
                    Title = u.Title,
                    Categories = u.Categories
                        .Select(s => s.Category.Name)
                        .ToList()
                })
                .OrderByDescending(p => p.Priority)
                .ToListAsync();

            return requests;
        }

        public async Task<List<RequestGetDTO>> GetAllApproved()
        {
            var requests = await _context.Requests
                .Include(i => i.Categories)
                .ThenInclude(ti => ti.Category)
                .Where(r => r.Status == RequestStatus.InPreparation || r.Status == RequestStatus.Delivering)
                .Select(u => new RequestGetDTO
                {
                    Id = u.Id,
                    Author = u.Author,
                    Priority = u.Priority.ToString(),
                    Status = u.Status.ToString(),
                    Title = u.Title,
                    Categories = u.Categories
                        .Select(s => s.Category.Name)
                        .ToList()
                })
                .ToListAsync();

            return requests;
        }

        public async Task<List<RequestGetDTO>> GetAllDiscarded()
        {
            var requests = await _context.Requests
                .Include(i => i.Categories)
                .ThenInclude(ti => ti.Category)
                .Where(r => r.Status == RequestStatus.Discarded)
                .Select(u => new RequestGetDTO
                {
                    Id = u.Id,
                    Author = u.Author,
                    Priority = u.Priority.ToString(),
                    Status = u.Status.ToString(),
                    Title = u.Title,
                    Categories = u.Categories
                        .Select(s => s.Category.Name)
                        .ToList()
                })
                .ToListAsync();

            return requests;
        }

        public async Task<RequestDetailsDTO> GetDetails(int requestId, string userId)
        {
            var requests = await _context.Requests
                .Include(c => c.Followers)
                .Include(b => b.Upvoters)
                .Include(i => i.Categories)
                .ThenInclude(ti => ti.Category)
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
                    Upvotes = u.Upvoters
                        .Where(u => u.RequestId == requestId)
                        .Count(),
                    Upvoted = u.Upvoters
                        .Select(s => s.UserId)
                        .Contains(userId),
                    Followed = u.Followers
                        .Select(v => v.UserId)
                        .Contains(userId),
                    Categories = u.Categories
                        .Select(s => s.Category.Name)
                        .ToList()
                })
                .FirstOrDefaultAsync(r => r.Id == requestId);

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

        public async Task<bool> Exists(string requestName)
        {
            var exists = await _context.Requests
                .Select(s => s.Title)
                .ContainsAsync(requestName);

            return exists;
        }

        public async Task Add(RequestAddDTO model)
        {
            var request = new Request
            {
                Title = model.Title,
                Priority = (Priority)Enum.Parse(typeof(Priority), model.Priority),
                Author = model.Author,
                Link = model.Link,
                DateAdded = DateTime.UtcNow,
                Motivation = model.Motivation
            };

            var categories = model.CategoryIds
                .Select(c => new RequestCategory
                {
                    CategoryId = c,
                    Request = request
                })
                .ToList();

            request.Categories = categories;

            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckMotivation(string priority, string motivation)
        {
            if (priority == "Critical" && motivation == null)
            {
                return false;
            }

            return true;
        }

        public async Task Edit(int id, string status)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return;
            }
            var parsedStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), status);
            request.Status = parsedStatus;
            await _context.SaveChangesAsync();
        }

        public async Task Follow(string userId, int requestId)
        {
            var followed = await _context.RequestsFollows
                .Where(r => r.RequestId == requestId)
                .Select(c => c.UserId)
                .ContainsAsync(userId);

            if (followed == true)
            {
                var followToRemove = await _context.RequestsFollows
                    .Where(w => w.RequestId == requestId)
                    .FirstOrDefaultAsync();

                _context.RequestsFollows.Remove(followToRemove);
                await _context.SaveChangesAsync();
            }
            else
            {
                var followToAdd = new RequestFollow
                {
                    RequestId = requestId,
                    UserId = userId
                };
                await _context.RequestsFollows.AddAsync(followToAdd);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Upvote(string userId, int requestId)
        {
            var upvoted = await _context.RequestsUpvotes
                .Where(r => r.RequestId == requestId)
                .Select(c => c.UserId)
                .ContainsAsync(userId);

            if (upvoted == true)
            {
                var upvoteToRemove = await _context.RequestsUpvotes
                    .Where(w => w.RequestId == requestId)
                    .FirstOrDefaultAsync();

                _context.RequestsUpvotes.Remove(upvoteToRemove);
                await _context.SaveChangesAsync();
            }
            else
            {
                var upvoteToAdd = new RequestUpvote
                {
                    RequestId = requestId,
                    UserId = userId
                };
                await _context.RequestsUpvotes.AddAsync(upvoteToAdd);
                await _context.SaveChangesAsync();
            }
        }
    }
}
