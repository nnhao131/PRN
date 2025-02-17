using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRipository.IUser
{
    public interface IRegister
    {
        Task AddUser(User user);
        Task<User> GetUserName(string username);
    }
}
