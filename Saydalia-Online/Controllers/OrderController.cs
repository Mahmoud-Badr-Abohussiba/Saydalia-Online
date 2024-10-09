using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saydalia_Online.Interfaces.InterfaceServices;
using Saydalia_Online.Models;
using System.Security.Claims;
using static NuGet.Packaging.PackagingConstants;

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

        public async Task<IActionResult> Index(int? page)
        {
            if(User.IsInRole("Pharmacist") || User.IsInRole("Admin"))
            {
                var orders = await _orderService.getOrdersAsync(page??1);
                return View(orders);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var orders = await _orderService.getOrdersAsync(userId,page??1);
                return View(orders);
            }
           
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.getDetailsByIdWithItems(id);
            return View(order);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _orderService.getDetailsByIdWithItems(id);
            return View(order);
        }

        public async Task<IActionResult> UpdateStatus(int id,string Status)
        {
            var order = await _orderService.getDetailsByIdWithItems(id);

            var validStatusesForPharmacist = new List<string> { "InTransit", "Canceled", "Rejected", "Delivered" };

            if (User.IsInRole("Pharmacist") || User.IsInRole("Admin"))
            {
                if (!validStatusesForPharmacist.Contains(Status))
                {
                    ModelState.AddModelError(string.Empty, "Invalid status");
                    return View(order); 
                }
            }
            else
            {
                Status = "Canceled";
            }

            order.Status = Status;
            await _orderService.UpdateOrder(order);

            return RedirectToAction("Index");   
        }


        public async Task<IActionResult> UpdatePaidStatus(int id)
        {
            var order = await _orderService.getDetailsByIdWithItems(id);


            order.Status = "Paid";
            await _orderService.UpdateOrder(order);

            return Json(new { success = true, message = "Order status updated successfully." });
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
            return RedirectToAction("Index", "CheckOut", new { totalAmount = order.TotalAmount , orderId = order.Id});
        }


        public async Task<IActionResult> CompleteOrder()
        {

            return View("OrderCompleted");
        }

        [HttpGet]
        public async Task<JsonResult> GetCartItemCount()
        {
            int count = 0;
            if (User.Identity.IsAuthenticated)
            {
                // Get the logged-in user's ID from the claims
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var order = await _orderService.GetInCartOrderAsync(userId);
                count = order.OrderItems.Count();
            }

            return Json(count);
        }


    }
}
