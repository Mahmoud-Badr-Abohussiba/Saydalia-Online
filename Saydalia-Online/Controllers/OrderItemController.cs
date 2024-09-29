using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saydalia_Online.Interfaces.InterfaceServices;
using System.Security.Claims;


namespace Saydalia_Online.Controllers
{
    [Authorize]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService) 
        {
            _orderItemService = orderItemService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var orderItem = await _orderItemService.GetByIdAsyncWithMedicne(id);
            return View(orderItem);
        }

        public async Task<IActionResult> Update(int itemId,int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderItemService.UpdateOrderItemAsync(userId, itemId, quantity);
            return RedirectToAction("Cart", "Order");
        }

        public async Task<IActionResult> Delete(int itemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _orderItemService.DeleteOrderItemAsync(userId, itemId);
            return RedirectToAction("Cart", "Order");
        }
    }
}
