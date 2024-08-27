using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Models;
using System.Diagnostics;

namespace Saydalia_Online.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        SaydaliaOnlineContext context = new SaydaliaOnlineContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           var medicines = context.Medicines
                   .Include(e => e.Categories)
                   .ToList();
            return View(medicines);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Store()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact() 
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult ShopSingle()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
