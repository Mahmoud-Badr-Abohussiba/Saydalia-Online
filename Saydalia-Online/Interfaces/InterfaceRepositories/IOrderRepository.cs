using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceRepositories
{
    public interface IOrderRepository : IGenaricRepository<Order>
    {
        Order GetInCartOrder(string userId);
        Task<Order> GetInCartOrderAsync(string userId);
        Task<IEnumerable<Order>> getOrdersAsync(string userId);
        Task<IEnumerable<Order>> getOrdersAsync();

        Task<Order> getDetailsByIdWithItems(int id);

    }
}
