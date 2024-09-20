using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;
using System.Diagnostics;

namespace Saydalia_Online.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMedicineRepository _medicineRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger , IMedicineRepository medicineRepository , ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _medicineRepository = medicineRepository;
            _categoryRepository=categoryRepository;
        }


        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineRepository.GetAll();
            return View(medicines);
        }
        //OnActionExecuting function is being called when any action in it's containing controller called
        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var catgs = await _categoryRepository.GetAll();

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

        public async Task<IActionResult> Store()
        {
            var medicines = await _medicineRepository.GetAll();
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

        public async Task<IActionResult> DisplayUsingNameFromAToZ()
        {
            var medicines = await _medicineRepository.DisplayUsingNameFromAToZ();
            return View(nameof(Store), medicines);
        }
        public async Task<IActionResult> DisplayUsingNameFromZToA()
        {
            var medicines = await _medicineRepository.DisplayUsingNameFromZToA();
            return View(nameof(Store), medicines);
        }
        public async Task<IActionResult> DisplayUsingPriceLowToHigh()
        {
            var medicines = await _medicineRepository.DisplayUsingPriceLowToHigh();
            return View(nameof(Store) , medicines);
        }
        public async Task<IActionResult> DisplayUsingPriceHighToLow()
        {
            var medicines = await _medicineRepository.DisplayUsingPriceHighToLow();
            return View(nameof(Store) , medicines);
        }
        public async Task<IActionResult> DisplayAllBetweenTwoPrices(int minPrice , int maxPrice)
        {
            var medicines = await _medicineRepository.DisplayAllBetweenTwoPrices(minPrice , maxPrice);
            return View(nameof(Store) , medicines);
        }
    }
}
