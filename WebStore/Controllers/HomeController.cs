using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() //home/index
        {
            return View();
        }

        public IActionResult SecondAction(string id)
        {
            return Content($"Second action with parameter {id}");
        }
    }
}
