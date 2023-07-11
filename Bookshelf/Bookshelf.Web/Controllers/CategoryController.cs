using Bookshelf.Core.Services.Contracts;
using Bookshelf.Web.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            CategoryGetViewModel model = new CategoryGetViewModel();
            model.Categories = result;

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            CategoryAddViewModel model = new CategoryAddViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Error!";
                return View(model);
            }

            await _categoryService.Add(model.Category);

            TempData["success"] = "Category successfully added!";
            return RedirectToAction("Index");   
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.Delete(id);

            TempData["warning"] = "Category deleted!";
            return RedirectToAction("Index");
        }
    }
}
