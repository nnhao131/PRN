using DataAccess.DAO.Cart;
using DataAccess.IRipository.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ripository.Cart
{
    public class CartRepository : ICart
    {
        public void AddToCart(int IdUser, int ProductID, int Quantity) => CartDAO.Instance.AddToCart(IdUser, ProductID, Quantity);
       
    }
}
