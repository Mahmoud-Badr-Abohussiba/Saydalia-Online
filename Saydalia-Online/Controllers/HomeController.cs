using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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




        //private List<MedicineCategory> GetMedicineCategories()
        //{
        //    // Fetch or generate the list of medicine categories
        //    return new List<MedicineCategory>
        //{
        //    new MedicineCategory
        //    {
        //        Name = "Supplements",
        //        SubCategories = new List<MedicineCategory>
        //        {
        //            new MedicineCategory { Name = "Vitamins" },
        //            new MedicineCategory { Name = "Diet & Nutrition" },
        //            new MedicineCategory { Name = "Tea & Coffee" }
        //        }
        //    },
        //    new MedicineCategory { Name = "Diet & Nutrition" },
        //    new MedicineCategory { Name = "Tea & Coffee" }
        //};
        //}


        public IActionResult Index()
        {
            var medicines = context.Medicines
                    .Include(e => e.Categories)
                    .ToList();
            return View(medicines);
        }
        //OnActionExecuting function is being called when any action in it's containing controller called
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var catgs = context.categories.ToList();
            //var medicineCategories = GetMedicineCategories();

            ViewBag.MedicineCategories = catgs;

            base.OnActionExecuting(filterContext);
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
            var medicines = context.Medicines
                    .Include(e => e.Categories)
                    .ToList();
            return View(medicines);
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
