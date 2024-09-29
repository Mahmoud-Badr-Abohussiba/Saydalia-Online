using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceServices
{
    public interface IOrderService
    {
        Task<Order> getDetailsById(int id);
        Task<Order> getDetailsByIdWithItems(int id);
        Task<IEnumerable<Order>> getOrdersAsync(string userId);
        Task<IEnumerable<Order>> getOrdersAsync();
        Task<Order> CreateOrUpdateInCartOrderAsync(string userId, int medicineId,int quantity);

        Task<int> UpdateOrder(Order order);
        Task<Order> GetInCartOrderAsync(string userId);

        Order UpdateOrderTotalPrice(Order order);

        Task<Order> ConfrimAsync(string userId,string Address, string Phone);
    }
}
