using Azure;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO.Product;
using DataAccess.IRipository.IProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Common;

namespace BookSellingApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProduct _product;
        public IndexModel(IProduct product) => _product = product; 
        public PagedList<ProductDTO> products { get; set; }
        public List<ProductDTO> productNum {  get; set; }
        public List<Category> cate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string selectedCategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }
        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int pageNumber { get; set; } = 1;
        [BindProperty(SupportsGet = true)]
        public int pageSize { get; set; } = 9;
        public async Task  OnGetAsync()
        {   
               
            try
            {
                productNum = await _product.GetHotdeals(5);
                if (string.IsNullOrEmpty(SortOrder))
                {
                    SortOrder = "Position";
                }
                cate = await _product.GetAllCategories();
                if (!string.IsNullOrEmpty(selectedCategory))
                {
                    products = await _product.GetProducts(selectedCategory, pageNumber, pageSize, SortOrder);
                }
                else
                {
                    products = await _product.GetProducts(searchTerm, pageNumber, pageSize, SortOrder);
                }
                
             
            }
            catch
            {
               
            }
        }
        

    }
    }
