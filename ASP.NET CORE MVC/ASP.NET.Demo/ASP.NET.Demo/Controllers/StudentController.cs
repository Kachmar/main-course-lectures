using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Demo.Controllers
{
    public class StudentController : Controller
    {
        // GET
        public IActionResult Students()
        {
            return View();
        }
    }
}