using BusinessObject.Helper;
using BusinessObject.Models;
using BusinessObject.ViewModel;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO.Login
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();
        private UserDAO()
        {

        }
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }
        public async Task<User> LockUnLockUser(int id)
        {
            try
            {
                using (var context = new DbBookStoreContext())
                {
                    var user = await context.Users.SingleOrDefaultAsync(c => c.Id == id);
                    if (user != null)
                    {
                        user.Status = false;
                        context.Users.Update(user);
                        await context.SaveChangesAsync();
                        return user;
                    }
                    return null;

                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<User> LockUser(int id)
        {
            try
            {
                using(var context = new DbBookStoreContext())
                {
                    var user =  await context.Users.SingleOrDefaultAsync(c=>c.Id == id);
                    if (user != null) 
                    {
                        user.Status = true;
                        context.Users.Update(user);
                        await context.SaveChangesAsync();
                        return user;
                    }
                    return null;

                }
            }
            catch
            {
                throw;
            }
        }
        public List<User> getallUser()
        {
            using(var context = new DbBookStoreContext())
            {
                var users = context.Users
             .Where(user => user.IdRole==2)
             .Include(c => c.IdRoleNavigation) 
             .ToList();

                return users;
            }
        }
        public async Task<User> GetUserByID(int id)
        {
            using(var context = new DbBookStoreContext())
            {
                var user = await context.Users.FindAsync(id);
                return user;
            }
        }
        public async Task<User?> ChChangePass(string newpass,int  id)
        {
            using(var context =new DbBookStoreContext())
            {
                var users = await context.Users.FindAsync(id);
                users.Password = newpass;
                context.Update(users);
                await context.SaveChangesAsync();
                return users;
            }
        }
        public async Task<User> UpdateUser(User user,int id)
        {
            using(var context = new DbBookStoreContext())
            {
                try
                {
                    var update = await GetUserByID(id);
                    
                        update.Address = user.Address;
                        update.Phonenumber = user.Phonenumber;
                        update.Email = user.Email;

                   
                    context.Update(update);
                    await context.SaveChangesAsync();
                    return update;


                }
                catch
                {
                    throw;
                }
            }
        }
        public User GetUserByEmail(string email)
        {
            User? user = null;
            try
            {

                var context = new DbBookStoreContext();
                user = context.Users.SingleOrDefault(u => u.Email.Equals(email));
            }
            catch (Exception ex)
            {

            }
            return user;
        }
        public void UpdateUserPass(RegisterViewModel user, IEmailService service)
        {
            Random rand = new Random();
            int stringlen = rand.Next(4, 10);
            int randValue;
            string pass = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {

                // Generating a random number. 
                randValue = rand.Next(0, 26);

                // Generating random character by converting 
                // the random number into character. 
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string. 
                pass = pass + letter;
            }

            MailContent content = new MailContent
            {
                To = user.Email,
                Subject = "Reset Your PassWord. ",
                Body = "<strong>Your new pass is:  </strong>" + pass
            };
            User AccountTemp = new User();
            try
            {
                var context = new DbBookStoreContext();
                AccountTemp = context.Users.FirstOrDefault(u => u.Email.Equals(user.Email));
                AccountTemp.Password = pass;
                context.Users.Update(AccountTemp);
                context.SaveChanges();
                service.SendMail(content);

            }
            catch (Exception ex)
            {

            }

        }

        public User GetUserByName(string name)
        {
            User user = null;
            try
            {

                var context = new DbBookStoreContext();
                user = context.Users.SingleOrDefault(u => u.Username.Equals(name));
            }
            catch (Exception ex)
            {

            }
            return user;
        }

        public void Register(RegisterViewModel user)
        {
            try
            {
                User usercheck = GetUserByName(user.Username);
                if (usercheck == null)
                {
                    var context = new DbBookStoreContext();
                    User NewUser = new User();
                    NewUser.Username = user.Username;
                    NewUser.Address = user.Address;
                    NewUser.Password = user.Password;
                    NewUser.Email = user.Email;
                    NewUser.Phonenumber = user.Phonenumber;

                    context.Users.Add(NewUser);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("User is exist in db");
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool Login(string userName, string pass)
        {
            var context = new DbBookStoreContext();
            var user = context.Users.SingleOrDefault(s => s.Username.Equals(userName) && s.Password.Equals(pass));
            if (user != null && user.Status != true)
            {
                return true;
            }

            return false;

        }

        public async Task<int> GetTotalUser()
        {
            using(var context = new DbBookStoreContext())
            {
                return await context.Users.CountAsync();
            }
        }
    }
}
