using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceServices
{
    public interface IOrderItemService
    {
        Task<OrderItem> GetByIdAsyncWithMedicne(int id);

        Task<Order> UpdateOrderItemAsync(string userId, int itemId, int quantity);
        Task<Order> DeleteOrderItemAsync(string userId, int itemId);

        Task CancelOrderItemAsync(int itemId);
    }
}
