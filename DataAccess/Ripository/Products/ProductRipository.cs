
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.IRipository.IProducts;
using DataAccess.DAO.Product;
using BusinessObject.DTOs;

namespace DataAccess.Ripository.Products
{
    public class ProductRipository : IProduct
    {
        public Task<PagedList<ProductDTO>> GetProducts(string searchTerm, int pageNumber, int pageSize, string sortOrder) => ProductDAO.Instance.GetallProduct(searchTerm,pageNumber,pageSize,sortOrder);

        public Task<List<ProductDTO>> GetProductsSortedByPrice(string sortOrder) => ProductDAO.Instance.GetProductsSortedByPrice(sortOrder);
        public Task<List<ProductDTO>> GetHotdeals(int number) => ProductDAO.Instance.Hotdeals(number);
        public Task<ProductDTO> GetDetail(int id)=> ProductDAO.Instance.GetProsutByID(id);

        public Task<List<ProductDTO>> GetAllProduct() => ProductDAO.Instance.getallProduct();

        public Task<Product> CreateProduct(Product product) => ProductDAO.Instance.CreateProduct(product);

        public Task<Product> UpdateProduct(Product product) =>ProductDAO.Instance.UpdateProduct(product);
     
        public Task<bool> DeleteProduct(int id) => ProductDAO.Instance.deleteProduct(id);
       
        public Task<List<Category>> GetAllCategories() => ProductDAO.Instance.GetAllCategories();

        public Task<List<ProductDTO>> GetcateName(string nameCate) => ProductDAO.Instance.GetcateName(nameCate);

        public Task<int> GetTotalProduct() => ProductDAO.Instance.GetTotalProduct();
    }
}
