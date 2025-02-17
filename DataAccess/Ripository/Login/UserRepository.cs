using BusinessObject.Helper;
using BusinessObject.Models;
using BusinessObject.ViewModel;
using DataAccess.DAO.Login;
using DataAccess.IRipository.IUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ripository.Login
{
    public class UserRepository : IUser
    {
        public List<User> GetAll() => UserDAO.Instance.getallUser();
       

        public User getUserbyName(string name)
        {
            return UserDAO.Instance.GetUserByName(name);
        }

        public bool Login(string userName, string passWord)
        {
            return UserDAO.Instance.Login(userName, passWord);
        }

       
        public void Register(RegisterViewModel user)
        {
            UserDAO.Instance.Register(user);
        }
        public User getUserByEmail(string email)
        {
            return UserDAO.Instance.GetUserByEmail(email);
        }

        public void UpdatePass(RegisterViewModel user, IEmailService service)
        {
            UserDAO.Instance.UpdateUserPass(user, service);
        }

        public Task<User> UpdateUser(User user, int id) => UserDAO.Instance.UpdateUser(user,id);
        

        public Task<User> ChChangePass(string newpass, int id)=> UserDAO.Instance.ChChangePass(newpass,id);
       

        public Task<User> GetUserByID(int id) => UserDAO.Instance.GetUserByID(id);

        public Task<User> LockUser(int id) => UserDAO.Instance.LockUser(id);



        public Task<User> LockUnLockUser(int id) => UserDAO.Instance.LockUnLockUser(id);


    }
}
