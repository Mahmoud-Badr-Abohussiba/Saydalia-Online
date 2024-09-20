using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceServices
{
    public interface IOrderService
    {
        Task<Order> getDetailsById(int id);
    }
}
