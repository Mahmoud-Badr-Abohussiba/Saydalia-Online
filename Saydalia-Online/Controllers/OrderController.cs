using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saydalia_Online.Interfaces.InterfaceServices;
using Saydalia_Online.Models;
using System.Security.Claims;

namespace Saydalia_Online.Controllers
{
    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> AddToCart(int medicineId,int quantity)
        {
            // Get the logged-in user's ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                // Handle the case where the user is not logged in, though this shouldn't happen due to [Authorize]
                return Unauthorized();
            }

            var order = await _orderService.CreateOrUpdateInCartOrderAsync(userId, medicineId, quantity);

            // Redirect to the cart view or wherever appropriate
            return RedirectToAction("Index", "Medicine");
        }

        [HttpGet]

        public async Task<IActionResult> Cart()
        {
            // Get the logged-in user's ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderService.GetInCartOrderAsync(userId);
            return View(order);
        }


        public async Task<IActionResult> Confirm(string Address,string Phone)
        {
            // Get the logged-in user's ID from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderService.ConfrimAsync(userId,Address, Phone);
            // Redirect to the cart view or wherever appropriate
            return RedirectToAction("Index", "Medicine");
        }


    }
}
