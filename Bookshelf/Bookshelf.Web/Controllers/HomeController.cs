using Bookshelf.Core.Services.Contracts;
using Bookshelf.Web.Models;
using Bookshelf.Web.Models.Requests;
using Bookshelf.Web.Models.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Security.Claims;

namespace Bookshelf.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRequestService _requestService;
        private readonly IResourceService _resourceService;

        public HomeController(ILogger<HomeController> logger, IRequestService requestService, IResourceService resourceService)
        {
            _logger = logger;
            _requestService = requestService;
            _resourceService = resourceService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _requestService.GetFollowedById(userId);
            RequestGetViewModel model = new RequestGetViewModel
            {
                Requests = result
            };

            return View(model);
        }

        public async Task<IActionResult> Taken()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var result = await _resourceService.GetTakenById(userId);

            ResourceGetViewModel model = new ResourceGetViewModel
            {
                Resource = result
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}