using Microsoft.AspNetCore.Mvc;

namespace Bookshelf.Web.Controllers
{
    public class ResourceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddRequestedResource(int requestId)
        {
            throw new NotImplementedException();
        }
    }
}
