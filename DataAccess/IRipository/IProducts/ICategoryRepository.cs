using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRipository.IProducts
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCate();
        Task<Category> GetCateByID(int id);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategoryt(int id);
    }
}
