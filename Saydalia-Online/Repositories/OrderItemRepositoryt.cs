using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;

namespace Saydalia_Online.Repositories
{
    public class OrderItemRepositoryt : GenaricRepository<OrderItem> , IOrderItemRepository
    {
        private readonly SaydaliaOnlineContext _dbContext;

        public OrderItemRepositoryt(SaydaliaOnlineContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
