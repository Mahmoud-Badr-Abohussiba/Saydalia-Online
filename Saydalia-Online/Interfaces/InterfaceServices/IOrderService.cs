using PagedList;
using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceServices
{
    public interface IOrderService
    {
        Task<Order> getDetailsById(int id);
        Task<Order> getDetailsByIdWithItems(int id);
        Task<IPagedList<Order>> getOrdersAsync(string userId, int page);
        Task<IPagedList<Order>> getOrdersAsync(int page);
        Task<Order> CreateOrUpdateInCartOrderAsync(string userId, int medicineId,int quantity);

        Task<int> UpdateOrder(Order order);
        Task<Order> GetInCartOrderAsync(string userId);

        Order UpdateOrderTotalPrice(Order order);

        Task<Order> ConfrimAsync(string userId,string Address, string Phone);
    }
}
