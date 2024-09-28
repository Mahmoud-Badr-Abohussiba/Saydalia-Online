using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;

namespace Saydalia_Online.Repositories
{
    public class OrderItemRepositoryt : GenaricRepository<OrderItem>, IOrderItemRepository
    {
        private readonly SaydaliaOnlineContext _dbContext;

        public OrderItemRepositoryt(SaydaliaOnlineContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderItem> GetByIdAsyncWithMedicne(int id)
        {
            var orderItem = await _dbContext.OrderItems.Where(e => e.Id == id)
               .Include(e => e.Medicine)
               .Include(e => e.Order)
               .FirstOrDefaultAsync(); 
         
            return orderItem;
        }
    }
}
