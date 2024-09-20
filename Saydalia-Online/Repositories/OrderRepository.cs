using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Models;

namespace Saydalia_Online.Repositories
{
    public class OrderRepository : GenaricRepository<Order>, IOrderRepository
    {
        private readonly SaydaliaOnlineContext _dbContext;

        public OrderRepository(SaydaliaOnlineContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
