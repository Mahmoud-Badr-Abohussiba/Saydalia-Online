using PagedList;
using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceRepositories
{
    public interface IOrderRepository : IGenaricRepository<Order>
    {
        Order GetInCartOrder(string userId);
        Task<Order> GetInCartOrderAsync(string userId);
        Task<IPagedList<Order>> getOrdersAsync(string userId,int page);
        Task<IPagedList<Order>> getOrdersAsync(int page);

        Task<Order> getDetailsByIdWithItems(int id);

    }
}
