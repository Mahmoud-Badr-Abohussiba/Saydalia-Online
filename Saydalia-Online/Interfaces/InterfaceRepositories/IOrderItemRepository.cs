using Saydalia_Online.Models;

namespace Saydalia_Online.Interfaces.InterfaceRepositories
{
    public interface IOrderItemRepository : IGenaricRepository<OrderItem>
    {
        Task<OrderItem> GetByIdAsyncWithMedicne(int id);
    }
}
