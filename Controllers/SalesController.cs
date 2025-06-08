using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectPlanner.Controllers
{
    public class SalesController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
