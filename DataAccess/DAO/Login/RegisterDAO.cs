using BusinessObject.Models;
using DataAccess.Data;
using DataAccess.IRipository.IUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO.Login
{
    public class RegisterDAO
    {
        private static RegisterDAO instance = null;
        private static readonly object instanceLock = new object();
        private RegisterDAO() { }
        public static RegisterDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new RegisterDAO();
                    }
                }
                return instance;
            }
        }
        
        public async Task AddUser(User user)
        {
            try
            {
                User _user = await GetUserName(user.Username);
                if (_user == null)
                {
                    

                    var myuser = new DbBookStoreContext();
                    user.SetDefaults();
                    await myuser.Users.AddAsync(user);
                    await myuser.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserName(string username)
        {
            User user = null;
            try
            {
                var myuser = new DbBookStoreContext();
                user = await myuser.Users.SingleOrDefaultAsync(n => n.Username.Equals(username));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }


    }
}
