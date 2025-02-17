using BusinessObject.Models;
using DataAccess.DAO.Product;
using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO.Cart
{
    public class CartDAO
    {
        private static CartDAO instance = null;
        private static readonly object instanceLock = new object();
        private CartDAO() { }
        public static CartDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CartDAO();
                    }
                }
                return instance;
            }
        }
        public void AddToCart(int IdUser, int ProductID,int Quantity)
        {
            try
            {

                using (var contex = new DbBookStoreContext())
                {
                    var order = new Order
                    {
                        Id = IdUser,
                        TotalPrice =0,
                        DateOrder = DateTime.Now,
                        OrderStatus = "Pending"

                    };
                    contex.Orders.Add(order);
                    contex.SaveChanges();
                    var newOrderId = order.Id;
                    var orderDetail = new OrderDetail
                    {
                        IdOrder = newOrderId,
                        IdProduct = ProductID,
                        Quantity = Quantity,
                        Price = 0, // Tính toán giá dựa trên sản phẩm và số lượng
                        Comment = null, // Bạn có thể cung cấp một ghi chú nếu cần
                        DateComment = null,
                        StatusWishlist = "true"
                    };
                    contex.OrderDetails.Add(orderDetail); contex.SaveChanges();

                }
            }
            catch
            {

            }
        }
    }
}
