using Microsoft.AspNetCore.Mvc;

namespace GraphQLAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
