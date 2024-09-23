using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceRepositories
{
    public interface IOrderRepository : IGenaricRepository<Order>
    {
        Order GetInCartOrder(string userId);
    }
}
