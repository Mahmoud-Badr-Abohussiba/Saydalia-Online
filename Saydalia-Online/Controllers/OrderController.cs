using Microsoft.AspNetCore.Mvc;
using Saydalia_Online.Interfaces.InterfaceServices;

namespace Saydalia_Online.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService; 
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.getDetailsById(id);
            return View(order);
        }


    }
}
