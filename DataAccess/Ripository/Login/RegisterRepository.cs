using BusinessObject.Models;
using DataAccess.DAO.Login;
using DataAccess.IRipository.IUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ripository.Login
{
    public class RegisterRepository : IRegister
    {
        public Task AddUser(User user) => RegisterDAO.Instance.AddUser(user);


        public Task<User> GetUserName(string username) => RegisterDAO.Instance.GetUserName(username);

        
    }
}
