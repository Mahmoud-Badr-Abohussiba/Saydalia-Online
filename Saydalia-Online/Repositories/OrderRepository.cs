using Microsoft.EntityFrameworkCore;
using PagedList;
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
            var order =  _dbContext.Orders.Where(e => e.UserID == userId && e.Status == "In Cart")
                .Include(e => e.OrderItems)
                .FirstOrDefault();
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
                                        .Include(e=>e.OrderItems)
                                        .FirstOrDefault();
            }
            return order;
        }

        public async Task<Order> GetInCartOrderAsync(string userId )
        {
            var order = await _dbContext.Orders.Where(e => e.UserID == userId && e.Status == "In Cart")
                .Include(e => e.OrderItems)
                .ThenInclude(oi=>oi.Medicine)
                .FirstOrDefaultAsync(); // gpt handle it so that the orederItems returned with related medicines
            if (order == null)
            {
                var newOrder = new Order()
                {
                    Status = "In Cart",
                    UserID = userId,
                };

                // Save the newly created order
                await _dbContext.Orders.AddAsync(newOrder);
                await _dbContext.SaveChangesAsync();  // Ensure the order is persisted in the database

                // Now retrieve the newly created order from the DB
                order = await _dbContext.Orders
                                        .Where(e => e.UserID == userId && e.Status == "In Cart")
                                        .Include(e => e.OrderItems)
                                        .FirstOrDefaultAsync();
            }
            return order;
        }

        public async Task<IPagedList<Order>> getOrdersAsync(string userId,int page)
        {
            var orders =  _dbContext.Orders.Where(e => e.UserID == userId && e.Status != "In Cart")
               .OrderByDescending(e=>e.CreatedAt)
               .Include(e=>e.User)
               .Include(e => e.OrderItems)
               .ThenInclude(oi => oi.Medicine)
               .ToPagedList(page, 12);
            return orders;
        }

        public async Task<IPagedList<Order>> getOrdersAsync(int page)
        {
            var orders =  _dbContext.Orders.Where(e=>e.Status != "In Cart")
               .OrderByDescending(e => e.CreatedAt)
               .Include(e => e.User)
               .Include(e => e.OrderItems)
               .ThenInclude(oi => oi.Medicine)
               .ToPagedList(page, 12);
            return orders;
        }

        public async Task<Order> getDetailsByIdWithItems(int id)
        {
            var order = await _dbContext.Orders.Where(e => e.Id == id)
                 .Include(e => e.OrderItems)
                 .ThenInclude(oi => oi.Medicine)
                 .FirstOrDefaultAsync();
            return order;
        }
    }
}
