using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() //home/index
        {
            return Content("Hello from first controller");
        }

        public IActionResult SecondAction(string id)
        {
            return Content($"Second action with parameter {id}");
        }
    }
}
