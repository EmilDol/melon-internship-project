using Bookshelf.Core.DTOs.Resources;
using Bookshelf.Core.Services.Contracts;
using Bookshelf.Web.Models.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    [Authorize]
    public class ResourceController : Controller
    {
        private readonly IResourceService _resourceService;
        private readonly IRequestService _requestService;
        private readonly ICategoryService _categoryService;

        public ResourceController(IResourceService resourceService, ICategoryService categoryService, IRequestService requestService)
        {
            _resourceService = resourceService;
            _categoryService = categoryService;
            _requestService = requestService;
        }

        public async Task<IActionResult> Index()
        {
            ResourceGetViewModel model = new ResourceGetViewModel();
            var result = await _resourceService.GetAll();
            model.Resource = result;
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            ResourceDetailsViewModel model = new ResourceDetailsViewModel();
            var result = await _resourceService.GetDetails(id);
            model.Resource = result;
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            ResourceAddViewModel model = new ResourceAddViewModel();
            model.Resource = new ResourceAddDTO
            {
                Categories = await _categoryService.GetAll()
            };
            model.ComesFromRequest = false;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(ResourceAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Error!";
                model.Resource.Categories = await _categoryService.GetAll();
                return View(model);
            }

            if (model.Resource.Type == "Digital" && string.IsNullOrEmpty(model.Resource.FilePath))
            {
                ModelState.AddModelError(nameof(model.Resource.FilePath), "Requiered file when the type is digital");
                model.Resource.Categories = await _categoryService.GetAll();
                return View(model);
            }

            if (model.ComesFromRequest)
            {
                await _requestService.Delete(model.Id);
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
            ResourceEditViewModel model = new ResourceEditViewModel();
            model.Resource = await _resourceService.GetEdit(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ResourceEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Error!";
                model.Resource.Categories = await _categoryService.GetAll();
                return View(model);
            }

            await _resourceService.Edit(model.Resource);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddRequestedResource(int requestId)
        {
            var request = await _requestService.GetRequestForResource(requestId);
            var model = new ResourceAddViewModel();
            model.ComesFromRequest = true;
            model.Id = requestId;
            model.Resource = request;
            return View(model);
        }
    }
}
