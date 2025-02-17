using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.DAO.Product;
using DataAccess.IRipository.IProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ripository.Products
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<Category> CreateCategory(Category category)=>CategoryDAO.Instance.CreateCategory(category);
        

        public Task<bool> DeleteCategoryt(int id)=>CategoryDAO.Instance.DeleteCategoryt(id);
        

        public Task<Category> GetCateByID(int id)=>CategoryDAO.Instance.GetCateByID(id);
        

        public Task<List<Category>> GetAllCate() => CategoryDAO.Instance.GetAllCategories();
        

        public Task<Category> UpdateCategory(Category category) =>CategoryDAO.Instance.UpdateCategory(category);
       
    }
}
