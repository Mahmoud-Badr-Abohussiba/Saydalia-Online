using Microsoft.AspNetCore.Identity;
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

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository,IMedicineRepository medicineRepository , SaydaliaOnlineContext dbContext)
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

        public  Order CreateOrUpdateInCartOrder(string userId, int medicineId,int quantity)
        {
            //try
            //{
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

                var medicine =  _medicineRepository.GetByIdSync(medicineId);

                //if(medicine.Stock >= quantity)
                //{
                
                    var inCartOrder =  _orderRepository.GetInCartOrder(userId);
                  
                    // check if medicine id is exist in order items 
                    // if exsits update the quantity
                    // if not exists create orderItem and assgin it to the product

                    var orderItems = inCartOrder.OrderItems;
                    

                    if(orderItems == null)
                    {
                        var newOrderItem = new OrderItem()
                        {
                            Quantity = quantity,
                            Price = quantity * medicine.Price,
                            OrderID = inCartOrder.Id,
                            MedicineID = medicineId,
                        };

                        _orderItemRepository.AddSync(newOrderItem);
            
                    }
                    else
                    {
                        var orderItem = orderItems.Where(i => i.MedicineID == medicineId).FirstOrDefault();
                        if (orderItem == null)
                        {
                            var newOrderItem = new OrderItem()
                            {
                                Quantity = quantity,
                                Price = quantity * medicine.Price,
                                OrderID = inCartOrder.Id,
                                MedicineID = medicineId,
                            };

                            _orderItemRepository.AddSync(newOrderItem);
                        }
                        else
                        {
                            orderItem.Quantity = quantity;
                            orderItem.Price = quantity * medicine.Price;
                            orderItem.UpdatedAt = DateTime.Now;
                            _orderItemRepository.UpdateSync(orderItem);
                        }
                    }
                   

                    return inCartOrder;

                //}
                //else
                //{
                //    throw new Exception("Quntity is not available");
                //}
            //}
            //catch (Exception ex)
            //{
            //    // we should use logger
            //    throw new Exception(ex.Message);
            //}
         
           


        }
    }
}
