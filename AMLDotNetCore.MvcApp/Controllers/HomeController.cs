using AMLDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AMLDotNetCore.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeResponseModel model = new HomeResponseModel();
            model.message = "hello from model";
            return View(model);
        }

        public IActionResult Privacy()
        {
            ViewBag.text = "Privacy Data";
            return View();
        }

        public IActionResult About()
        {
            ViewBag.text = "About Us";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
