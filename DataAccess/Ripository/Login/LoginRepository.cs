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
    public class LoginRepository : ILogin
    {
        public Task<User> Login(string username, string password) => LoginDAO.Instance.Login(username, password);
        
    }
}
