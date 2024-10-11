using Microsoft.AspNetCore.Identity;
using PagedList;
using Saydalia_Online.Interfaces.InterfaceRepositories;
using Saydalia_Online.Interfaces.InterfaceServices;
using Saydalia_Online.Models;
using System.Net;

namespace Saydalia_Online.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMedicineRepository _medicineRepository;
        private readonly SaydaliaOnlineContext _dbContext;

        public OrderService(IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IMedicineRepository medicineRepository ,
            SaydaliaOnlineContext dbContext)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _medicineRepository = medicineRepository;
            _dbContext = dbContext;
        }

        public async Task<Order> getDetailsById(int id)
        {
            return  await _orderRepository.GetById(id);
        }
        public async Task<Order> getDetailsByIdWithItems(int id)
        {
            return await _orderRepository.getDetailsByIdWithItems(id);
        }
        public async Task<Order> GetInCartOrderAsync(string userId)
        {
            return await _orderRepository.GetInCartOrderAsync(userId);
        }

        public async Task<Order> CreateOrUpdateInCartOrderAsync(string userId, int medicineId, int quantity)
        {

            try
            {

                var medicine = await _medicineRepository.GetById(medicineId);

                if (medicine.Stock >= quantity)
                {
                    var inCartOrder = await _orderRepository.GetInCartOrderAsync(userId);

                    // check if medicine id is exist in order items 
                    // if exsits update the quantity
                    // if not exists create orderItem and assgin it to the product

                    var orderItems = inCartOrder.OrderItems;

                    if (orderItems == null)
                    {
                        var newOrderItem = new OrderItem()
                        {
                            Quantity = quantity,
                            Price = quantity * medicine.Price,
                            OrderID = inCartOrder.Id,
                            MedicineID = medicineId,
                        };

                        await _orderItemRepository.Add(newOrderItem);

                    }
                    else
                    {
                        var orderItem =  orderItems.Where(i => i.MedicineID == medicineId).FirstOrDefault();
                        if (orderItem == null)
                        {
                            var newOrderItem = new OrderItem()
                            {
                                Quantity = quantity,
                                Price = quantity * medicine.Price,
                                OrderID = inCartOrder.Id,
                                MedicineID = medicineId,
                            };

                            await _orderItemRepository.Add(newOrderItem);
                        }
                        else
                        {
                            orderItem.Quantity = quantity;
                            orderItem.Price = quantity * medicine.Price;
                            orderItem.UpdatedAt = DateTime.Now;
                            await  _orderItemRepository.Update(orderItem);
                        }
                    }

                    medicine.Stock -= quantity;
                    await _medicineRepository.Update(medicine);

                    inCartOrder = this.UpdateOrderTotalPrice(inCartOrder);
                    return inCartOrder;

                }
                else
                {
                    throw new Exception("Quntity is not available");
                }
            }
            catch (Exception ex)
            {
                // we should use logger
                throw new Exception(ex.Message);
            }
        }
    
        public Order UpdateOrderTotalPrice(Order order)
        {
            var total = 0;
            if(order.OrderItems.Count > 0)
            {
                foreach (var item in order.OrderItems)
                {
                    total += (int)item.Price;
                }
            }
            order.TotalAmount = total;
            _dbContext.SaveChanges();
            return order;
        }

        public async Task<Order> ConfrimAsync(string userId,string Address, string Phone)
        {
            var inCartOrder = await _orderRepository.GetInCartOrderAsync(userId);
            inCartOrder.Address = Address;
            inCartOrder.Phone = Phone;
            inCartOrder.Status = "Need Payment";
            inCartOrder.OrderDate = DateTime.Now;
            await _orderRepository.Update(inCartOrder);
            return inCartOrder;
        }

        public async Task<IPagedList<Order>> getOrdersAsync(string userId,int page)
        {
            return await _orderRepository.getOrdersAsync(userId, page);
        }

        public async Task<IPagedList<Order>> getOrdersAsync(int page)
        {
            return await _orderRepository.getOrdersAsync(page);   
        }

        public async Task<int> UpdateOrder(Order order)
        {
            if(order.Status == "Canceled" || order.Status == "Rejected")
            {
                foreach (var item in order.OrderItems)
                {
                    item.Medicine.Stock += item.Quantity;
                    await _medicineRepository.Update(item.Medicine);
                }
            }
            var ord = await _orderRepository.Update(order);
            return ord;
        }
    }

}
