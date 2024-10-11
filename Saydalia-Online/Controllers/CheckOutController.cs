using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Interfaces.InterfaceServices;
using System.Text;
using System.Text.Json.Nodes;

namespace Saydalia_Online.Controllers
{
    public class CheckOutController : Controller
    {
        private string _paypalClientId { get; set; } = "";
        private string _paypalSecret { get; set; } = "";
        private string _paypalUrl { get; set; } = "";
        private IPayService _paymentService;
        private readonly ICategoryRepository _categoryRepository;


        public CheckOutController(IPayService payService, IConfiguration configuration, ICategoryRepository categoryRepository) 
        {
            _paypalClientId = configuration["PaypalSettings:ClientId"]!;
            _paymentService = payService;
            _categoryRepository = categoryRepository;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var catgs = await _categoryRepository.GetAll();
            ViewBag.MedicineCategories = catgs;

            await next(); // This ensures the action method executes after your logic
        }

        [HttpGet]
        public IActionResult Index(decimal totalAmount,int orderId)
        {
            ViewBag.ClientID = _paypalClientId;
            ViewBag.TotalAmount = totalAmount;
            ViewBag.SaydaliaOrderId = orderId;
            return View();
        }

        public async Task<JsonResult> CreateOrder([FromBody] JsonObject data)
        {
            return await _paymentService.CreateOrder(data);
        }


        public async Task<JsonResult> CompleteOrder([FromBody] JsonObject data)
        {
            return await _paymentService.CompleteOrder(data);
        }



    }
}
