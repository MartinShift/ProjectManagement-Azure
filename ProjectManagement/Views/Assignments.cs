using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Views
{
    public class Assignments : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
