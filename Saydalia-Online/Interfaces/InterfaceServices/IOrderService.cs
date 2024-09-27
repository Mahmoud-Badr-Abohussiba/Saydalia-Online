using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceServices
{
    public interface IOrderService
    {
        Task<Order> getDetailsById(int id);
        Order CreateOrUpdateInCartOrder(string userId, int medicineId,int quantity);
        Task<Order> CreateOrUpdateInCartOrderAsync(string userId, int medicineId,int quantity);
    }
}
