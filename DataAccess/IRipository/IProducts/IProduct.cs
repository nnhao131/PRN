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
    public interface IProduct
    {
        Task<List<Category>> GetAllCategories();
        Task<PagedList<ProductDTO>> GetProducts(string searchTerm, int pageNumber, int pageSize,string sortOrder);
        Task<List<ProductDTO>> GetAllProduct();
        Task<List<ProductDTO>> GetProductsSortedByPrice(string sortOrder);
        Task<List<ProductDTO>> GetHotdeals(int number);
        Task<List<ProductDTO>> GetcateName(string nameCate);
        Task<ProductDTO> GetDetail(int id);
        Task<Product> CreateProduct(Product product); 
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);

        
    }
}
