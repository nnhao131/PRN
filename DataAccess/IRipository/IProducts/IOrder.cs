using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRipository.IProducts
{
    public interface IOrder
    {
        Task<Order> OrderCreateAsync(int id,decimal total);
        Task<OrderDetail> OrderDetailCreateAsync(int idOrder, int idProduct, int quantity, decimal price);

        List<Order> GetAllOrder();
        Task<Order> UpdateOrder(int id);
        Task<Order>GetOrderByID(int idOrder);
        Task<List<Order>> GetOrderByIdUser(int idUser);
        
    }
}
