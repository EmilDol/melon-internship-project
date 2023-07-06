using System.Security.Claims;

using Bookshelf.Core.Services.Contracts;
using Bookshelf.Infrastructure.Models.Enums;
using Bookshelf.Web.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var requests = await _requestService.GetAllUnapproved();
            RequestGetViewModel requestsModel = new RequestGetViewModel
            {
                Requests = requests
            };
            return View(requestsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Approved()
        {
            var requests = await _requestService.GetAllApproved();
            RequestGetViewModel requestsModel = new RequestGetViewModel
            {
                Requests = requests
            };
            return View(requestsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Discarded()
        {
            var requests = await _requestService.GetAllDiscarded();
            RequestGetViewModel requestsModel = new RequestGetViewModel
            {
                Requests = requests
            };
            return View(requestsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            RequestDetailsViewModel request = new RequestDetailsViewModel
            {
                Request = await _requestService.GetDetails(id, userId),
            };

            return View(request);
        }

        public async Task<IActionResult> StatusUpdate(int id)
        {
            var status = await _requestService.StatusUpdate(id);

            if (status == RequestStatus.Delivered)
            {
                return RedirectToAction(nameof(ResourceController.AddRequestedResource),
                    "Resource", new { requestId  = id});
            }

            return RedirectToAction(nameof(Approved));
        }


        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            await _requestService.Approve(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
