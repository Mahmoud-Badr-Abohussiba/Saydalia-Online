using Microsoft.EntityFrameworkCore;
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

        public  Order GetInCartOrder(string userId)
        {
            var order =  _dbContext.Orders.Where(e => e.UserID == userId).FirstOrDefault();
            if (order == null)
            {
                var newOrder = new Order()
                {
                    Status = "In Cart",
                    UserID = userId,
                };

                // Save the newly created order
                 _dbContext.Orders.Add(newOrder);
                 _dbContext.SaveChanges();  // Ensure the order is persisted in the database

                // Now retrieve the newly created order from the DB
                order =  _dbContext.Orders
                                        .Where(e => e.UserID == userId && e.Status == "In Cart")
                                        .FirstOrDefault();
            }
            return order;
        }
    }
}
