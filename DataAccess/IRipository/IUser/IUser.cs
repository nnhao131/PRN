using BusinessObject.DTOs;
using BusinessObject.Helper;
using BusinessObject.Models;
using BusinessObject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRipository.IUser
{
    public interface  IUser
    {
        public bool Login(string userName, string passWord);
       

        public User getUserbyName(string name);

        public void Register(RegisterViewModel user);
        public User getUserByEmail(string email);

        public void UpdatePass(RegisterViewModel user, IEmailService service);
        public List<User> GetAll();
        Task<User> LockUser(int id);
        Task<User> LockUnLockUser(int id);
        Task<User> UpdateUser(User user,int id);
        Task<User> ChChangePass(string newpass,int id);
        Task<User> GetUserByID(int id);
        Task<int> GetTotalUser();
    }
}
