using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRipository.IUser
{
    public interface ILogin
    {
        Task<User> Login(string username, string password);
    }
}
