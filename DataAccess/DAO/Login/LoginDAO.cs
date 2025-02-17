using BusinessObject.Models;
using DataAccess.Data;
using DataAccess.IRipository.IUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO.Login
{
    public class LoginDAO 
    {

        
        private static LoginDAO instance = null;
        private static readonly object instanceLock = new object();
        private LoginDAO() { }
        public static LoginDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new LoginDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<User> Login(string username, string password)
        {   

            try
            {
                var my= new DbBookStoreContext();
                var user = await my.Users.SingleOrDefaultAsync(c => c.Username.Equals(username) && c.Password.Equals(password));
                if (user != null)
                {

                    return user;

                }
                else
                {   

                    return null;
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
