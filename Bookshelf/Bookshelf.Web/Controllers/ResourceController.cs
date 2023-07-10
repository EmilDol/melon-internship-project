using System.Runtime.CompilerServices;

using Bookshelf.Core.Services.Contracts;
using Bookshelf.Web.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _resourceService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ResourceAddViewModel model = new ResourceAddViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ResourceAddViewModel model)
        {
            if(!ModelState.IsValid)
            {
                TempData["error"] = "Error!";
                return View(model);
            }

            await _resourceService.Add(model.Resource);

            TempData["success"] = "Resource successfully added!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _resourceService.Delete(id);

            TempData["warning"] = "Resource deleted!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ResourceGetViewModel model = new ResourceGetViewModel();
            model.Resource = await _resourceService.GetById(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ResourceGetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Error!";
                return View(model);
            }

            await _resourceService.Edit(model.Resource);

            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public async Task<IActionResult> AddRequestedResource(int requestId)
        //{
            

        //}
    }
}
