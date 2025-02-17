using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRipository.Cart
{
    public interface ICart
    {
        public void AddToCart(int IdUser, int ProductID, int Quantity);
    }
}
