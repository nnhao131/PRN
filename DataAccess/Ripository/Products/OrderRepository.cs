using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRipository.IProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ripository.Products
{
    public class OrderRepository : IOrder
    {
        public List<Order> GetAllOrder()=> OrderDAO.Instance.getallOrder();

        public Task<Order> GetOrderByID(int idOrder) => OrderDAO.Instance.GetOrderByID(idOrder);

        public Task<List<Order>> GetOrderByIdUser(int idUser) => OrderDAO.Instance.GetOrderByIdUser( idUser);

        public Task<int> GetOrdersInDay(int day) => OrderDAO.Instance.GetNewOrdersInDays(day);

        public Task<decimal> GetRevenueInDay(int day) => OrderDAO.Instance.GetRevenueInDays(day);

        public Task<int> GetTotalOrder() => OrderDAO.Instance.GetTotalOrders();

        public Task<decimal> GetTotalRevenue() => OrderDAO.Instance.GetTotalRevenue();

        public Task<Order> OrderCreateAsync(int id, decimal total) => OrderDAO.Instance.OrderCreate(id, total);
      
        public Task<OrderDetail> OrderDetailCreateAsync(int idOrder, int idProduct, int quantity, decimal price)=>OrderDAO.Instance.OrderCreateOrderDetail(idOrder, idProduct, quantity, price);

        public Task<Order> UpdateOrder(int id) => OrderDAO.Instance.UpdateOrder(id);
        
    }
}
