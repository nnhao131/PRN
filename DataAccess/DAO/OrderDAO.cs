using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO.Cart;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                }
                return instance;
            }
        }
        public async Task<List<Order>> GetOrderByIdUser(int idUser)
        {
            using(var context = new DbBookStoreContext())
            {
                var order = await context.Orders.Include(c => c.IdUserNavigation)
                                .Where(o => o.IdUserNavigation.Id == idUser).ToListAsync();
                return order;
            }
        }
        public async Task<Order> GetOrderByID(int idOrder)
        {
            using(var context = new DbBookStoreContext())
            {
                var order = await context.Orders.SingleOrDefaultAsync(c=>c.Id == idOrder);
                return order;              
            }
        }
        public async Task<Order> UpdateOrder(int id)
        {
            using (var context = new DbBookStoreContext())
            {
                var update = await context.Orders.SingleOrDefaultAsync(c => c.Id == id);
                if (update != null)
                {
                    update.OrderStatus = "successfully";
                }
                context.Orders.Update(update);
                await context.SaveChangesAsync();
                return update;
            }
        }
        public  List<Order> getallOrder()
        {
            using(var context = new DbBookStoreContext())
            {
                var order =  context.Orders.ToList();
                return order;
            }
        }
        private int GenerateRandomId()
        {
            // Sử dụng thư viện System.Random để tạo số ngẫu nhiên
            Random random = new Random();
            return random.Next(1000, 9999); // Số ngẫu nhiên từ 1000 đến 9999 (có thể điều chỉnh phạm vi theo nhu cầu)
        }
        public async Task<Order> OrderCreate(int UserID, decimal TotalPrice)
        {
            try
            {
                using(var context = new DbBookStoreContext())
                {
                    
                    var newOrder = new Order
                    {  
                        Id=GenerateRandomId(),
                        IdUser = UserID,
                        TotalPrice = TotalPrice,
                        DateOrder = DateTime.Now,
                        OrderStatus= "Pending"
                    };
                         context.AddAsync(newOrder);
                    await context.SaveChangesAsync();
                    return newOrder;
                }
               
            }catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<OrderDetail> OrderCreateOrderDetail(int OrderID, int productID,int quantity, decimal price)
        {
            try
            {
                using (var context = new DbBookStoreContext())
                {
                    var orderdetail = new OrderDetail
                    {   Id =GenerateRandomId(),
                        IdOrder = OrderID,
                        IdProduct =productID,
                        Quantity = quantity,
                        Price = price

                    };
                    context.AddAsync(orderdetail);
                    await context.SaveChangesAsync();
                    return orderdetail;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
           
        }

        public async Task<int> GetTotalOrders()
        {
            using(var context = new DbBookStoreContext())
            return await context.Orders.CountAsync();
        }

        public async Task<decimal> GetTotalRevenue()
        {
            using(var context = new DbBookStoreContext())
            return await context.Orders.Where(o => o.OrderStatus == "successfully").SumAsync(o => o.TotalPrice ?? 0);
        }


        public async Task<int> GetNewOrdersInDays(int days)
        {
            using (var context = new DbBookStoreContext())
            {
                var startDate = DateTime.Now.AddDays(-days);
                return await context.Orders
                    .Where(o => o.DateOrder >= startDate)
                    .CountAsync();
            }
        }

        public async Task<decimal> GetRevenueInDays(int days)
        {
            using(var context = new DbBookStoreContext()) { 
            var startDate = DateTime.Now.AddDays(-days);
            return await context.Orders
                .Where(o => o.DateOrder >= startDate && o.OrderStatus == "successfully")
                .SumAsync(o => o.TotalPrice ?? 0);
            }
        }


    }
}
