using System.Security.Claims;

using Bookshelf.Core.DTOs;
using Bookshelf.Core.DTOs.Requests;
using Bookshelf.Core.Services.Contracts;
using Bookshelf.Infrastructure.Models.Enums;
using Bookshelf.Web.Models.Requests;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly ICategoryService _categoryService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public RequestController(IRequestService requestService, ICategoryService categoryService, IEmailService emailService, IConfiguration configuration)
        {
            _requestService = requestService;
            _categoryService = categoryService;
            _emailService = emailService;
            _configuration = configuration;
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Rejected()
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> StatusUpdate(int id)
        {
            var status = await _requestService.GetStatus(id);

            if (status == RequestStatus.Delivering)
            {
                TempData["warning"] = "You are about to create a new resource!";
                return RedirectToAction(nameof(ResourceController.AddRequestedResource),
                    "Resource", new { requestId = id });
            }

            await _requestService.StatusUpdate(id);

            string smtpServer = _configuration.GetSection("EmailSettings:SmtpServer").Value;
            int port = int.Parse(_configuration.GetSection("EmailSettings:Port").Value);
            string email = _configuration.GetSection("EmailSettings:Email").Value;
            string password = _configuration.GetSection("EmailSettings:Password").Value;
            string body = "The resource is now being delivered";
            string subject = "Melon request";
            var receivers = await _requestService.GetFollowers(id);
            await _emailService.SendEmail(receivers, body, subject, smtpServer, email, password, port);

            TempData["success"] = "Status has been successfully updated!";
            return RedirectToAction(nameof(Approved));
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            await _requestService.Approve(id);

            string smtpServer = _configuration.GetSection("EmailSettings:SmtpServer").Value;
            int port = int.Parse(_configuration.GetSection("EmailSettings:Port").Value);
            string email = _configuration.GetSection("EmailSettings:Email").Value;
            string password = _configuration.GetSection("EmailSettings:Password").Value;

            string body = "The resource has been approved and is now in preparation";
            string subject = "Melon request";
            var receivers = await _requestService.GetFollowers(id);
            await _emailService.SendEmail(receivers, body, subject, smtpServer, email, password, port);


            TempData["info"] = "You have successfully approved the request!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id)
        {
            await _requestService.Reject(id);

            string smtpServer = _configuration.GetSection("EmailSettings:SmtpServer").Value;
            int port = int.Parse(_configuration.GetSection("EmailSettings:Port").Value);
            string email = _configuration.GetSection("EmailSettings:Email").Value;
            string password = _configuration.GetSection("EmailSettings:Password").Value;

            string body = "The resource has been rejected";
            string subject = "Melon request";
            var receivers = await _requestService.GetFollowers(id);
            await _emailService.SendEmail(receivers, body, subject, smtpServer, email, password, port);

            TempData["warning"] = "The request has been rejected!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var modelForView = new RequestAddViewModel();
            modelForView.Request = new RequestAddDTO();
            modelForView.Request.Categories = await _categoryService.GetAll();

            return View(modelForView);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RequestAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Error!";
                return View(model);
            }

            if (await _requestService.Exists(model.Request.Title))
            {
                //tempdata  
                ModelState.AddModelError(nameof(model.Request.Title), "This resource already exists");

                TempData["error"] = "This resource already exists!";
                return View(model);
            }

            if (await _requestService.CheckMotivation(model.Request.Priority, model.Request.Motivation) == false)
            {
                ModelState.AddModelError(nameof(model.Request.Title), "Resource requires motivation because of its critical priority!");

                TempData["error"] = "Resource requires motivation because of its critical priority!";
                return View(model);
            }

            await _requestService.Add(model.Request);

            TempData["success"] = "The request has been successfully added!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, string status)
        {
            await _requestService.Edit(id, status);

            string smtpServer = _configuration.GetSection("EmailSettings:SmtpServer").Value;
            int port = int.Parse(_configuration.GetSection("EmailSettings:Port").Value);
            string email = _configuration.GetSection("EmailSettings:Email").Value;
            string password = _configuration.GetSection("EmailSettings:Password").Value;

            string body = "Request's status has been updated";
            string subject = "Melon request";
            var receivers = await _requestService.GetFollowers(id);
            await _emailService.SendEmail(receivers, body, subject, smtpServer, email, password, port);

            TempData["success"] = "The request has been successfully edited!";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Follow(int id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _requestService.Follow(userId, id);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Upvote(int id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _requestService.Upvote(userId, id);

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
