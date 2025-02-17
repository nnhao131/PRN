using BusinessObject.Models;
using DataAccess.DAO.Product;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                }
                return instance;
            }
        }
        public async Task<List<Category>> GetAllCategories()
        {
            using (var context = new DbBookStoreContext())
            {
                return await context.Categories.ToListAsync();
            }
        }
        private int GenerateRandomId()
        {
            // Sử dụng thư viện System.Random để tạo số ngẫu nhiên
            Random random = new Random();
            return random.Next(10000, 99999);
        }
        public async Task<Category> CreateCategory(Category category)
        {
            using(var context = new DbBookStoreContext()) {

                try
                {
                    category.Id=GenerateRandomId();
                    context.Add(category);
                    await context.SaveChangesAsync();
                    return category;
                }
                catch
                {
                    throw;
                }
            }
        }
        public async Task<bool> DeleteCategoryt(int id)
        {
            using(var context = new DbBookStoreContext())
            {
                var cateid= await context.Categories.SingleOrDefaultAsync(c=>c.Id==id);
                if (cateid != null)
                {
                    context.Remove(cateid);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public async Task<Category> GetCateByID(int id)
        {
            using(var context = new DbBookStoreContext())
            {
                var category = await context.Categories.FindAsync(id);
                return category;
            }
        }
        public async Task<Category> UpdateCategory(Category category)
        {
            using(var  context = new DbBookStoreContext())
            {
                try
                {
                    var cateid = await context.Categories.FindAsync(category.Id);
                    if (cateid != null)
                    {
                        cateid.CategoryName = category.CategoryName;
                        cateid.Description= category.Description;
                        context.Update(cateid);
                        await context.SaveChangesAsync();
                        
                    }
                    return cateid;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
