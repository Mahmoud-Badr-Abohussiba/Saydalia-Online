using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Interfaces.InterfaceServices;
using System.Security.Claims;


namespace Saydalia_Online.Controllers
{
    [Authorize]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;
        private readonly ICategoryRepository _categoryRepository;

        public OrderItemController(IOrderItemService orderItemService, ICategoryRepository categoryRepository) 
        {
            _orderItemService = orderItemService;
            _categoryRepository = categoryRepository;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var catgs = await _categoryRepository.GetAll();
            ViewBag.MedicineCategories = catgs;

            await next(); // This ensures the action method executes after your logic
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
