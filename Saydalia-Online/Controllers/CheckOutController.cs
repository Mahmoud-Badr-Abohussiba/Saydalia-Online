using Microsoft.AspNetCore.Mvc;
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


        public CheckOutController(IPayService payService, IConfiguration configuration) 
        {
            _paypalClientId = configuration["PaypalSettings:ClientId"]!;
            _paymentService = payService;
        }

        public IActionResult Index()
        {
            ViewBag.ClientID = _paypalClientId;
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
