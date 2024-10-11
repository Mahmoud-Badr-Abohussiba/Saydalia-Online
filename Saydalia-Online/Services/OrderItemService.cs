using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Interfaces.InterfaceServices;
using Saydalia_Online.Models;

namespace Saydalia_Online.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IOrderService _orderService;

        public OrderItemService(
            IOrderItemRepository orderItemRepository, 
            IMedicineRepository medicineRepository,
            IOrderService orderService
            )
        {
            _orderItemRepository = orderItemRepository;
            _medicineRepository = medicineRepository;
            _orderService = orderService;
        }

        public async Task<Order> DeleteOrderItemAsync(string userId, int itemId)
        {
            var item = await _orderItemRepository.GetByIdAsyncWithMedicne(itemId);
            item.Medicine.Stock += item.Quantity;
            await _medicineRepository.Update(item.Medicine);
            var order = await _orderService.GetInCartOrderAsync(userId);
            await _orderItemRepository.Delete(item);
            order = _orderService.UpdateOrderTotalPrice(order);

            return order;

        }

        public async Task CancelOrderItemAsync(int itemId)
        {
            var item = await _orderItemRepository.GetByIdAsyncWithMedicne(itemId);
            item.Medicine.Stock += item.Quantity;
            await _medicineRepository.Update(item.Medicine);
        }



        public async Task<OrderItem> GetByIdAsyncWithMedicne(int id)
        {
           return await _orderItemRepository.GetByIdAsyncWithMedicne(id);
        }

        public async Task<Order> UpdateOrderItemAsync(string userId, int itemId, int quantity)
        {
           var item = await _orderItemRepository.GetByIdAsyncWithMedicne(itemId);
            item.Medicine.Stock += item.Quantity;
            await _medicineRepository.Update(item.Medicine);
            var order = await _orderService.CreateOrUpdateInCartOrderAsync(userId, item.Medicine.Id, quantity);

            return order;
        }
    }
}
