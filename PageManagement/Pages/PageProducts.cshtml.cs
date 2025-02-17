using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO.Product;
using DataAccess.IRipository.IProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PageManagement.Pages
{
    
    public class PageProductsModel : PageModel
    {
        private readonly IProduct _product;
        private readonly ICategoryRepository _categoryRepository;
        public PageProductsModel(IProduct product, ICategoryRepository categoryRepository)
        {
            _product = product;
            _categoryRepository = categoryRepository;
        }
        public List<ProductDTO> products { get; set; }
        public Product input {  get; set; }
        public List<Category> category { get; set; }
        [HttpGet]
        public async Task<IActionResult> OnGet()
        {
            try
            {
                var userrole = HttpContext.Session.GetInt32("UserRoleADM");
                if (Convert.ToInt32(userrole) != 1)
                {

                    return RedirectToPage("/Login");
                }
                category = await _product.GetAllCategories();
                products = await _product.GetAllProduct();
                return Page();
                
            }
            catch(Exception ex) {

                throw new Exception(ex.Message, ex);
            
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> OnGetDetail(int id)
        {
            
            var model = await _product.GetDetail(id);
            
            return new JsonResult(model);
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpdate()
        {
            try
            {
             
             string id = Request.Form["ProductId"];
            string productname = Request.Form["Productname"];
            string Description = Request.Form["Description"];
            string Price = Request.Form["Price"]; 
            string Quantity = Request.Form["Quantity"];
            string IdCategory= Request.Form["cate"];
            var file = Request.Form.Files["img"];

                var update = new Product
                {   
                    Id = Convert.ToInt32(id),
                    ProductName = productname,
                    Price = Convert.ToDecimal(Price),
                    Description = Description,
                    Quantity = Convert.ToInt32(Quantity),
                    IdCategory = Convert.ToInt32(IdCategory),

                };
                if (file != null && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();


                        update.Image = fileBytes;
                    }
                }
                var createdProduct = await _product.UpdateProduct(update);
                
                return RedirectToPage("PageProducts");
            }
            catch
            {
                throw;
            }
        }
        
        
        public async Task<IActionResult> OnPostCreate()
        {
            string productname = Request.Form["Productname"];
            string Description = Request.Form["Description"];
            string Price = Request.Form["Price"]; 
            string Quantity = Request.Form["Quantity"];
            string IdCategory= Request.Form["cate"];
            var file = Request.Form.Files["img"];
            
           

            try
            {   
                var create = new Product
                {   
                    
                    ProductName = productname,
                    Price = Convert.ToDecimal(Price),
                    Description = Description,
                    Quantity = Convert.ToInt32(Quantity),
                    IdCategory = Convert.ToInt32(IdCategory),
                   
                };
                if (file != null && file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        byte[] fileBytes = ms.ToArray();


                        create.Image = fileBytes;
                    }
                }
                var createdProduct = await _product.CreateProduct(create);
                return new JsonResult(createdProduct);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> OnGetDetailP(int id)
        {
            var model = await _product.GetDetail(id);

            return new JsonResult(model);
        }
        [HttpGet]
        public async Task<IActionResult> OnGetDelete(int id)
        {
            var model = await _product.GetDetail(id);

            return new JsonResult(new {model});
        }
        [HttpPost]
        public async Task<IActionResult> OnPostDelete( )
        {
            string id = Request.Form["id"];
            int proid = Convert.ToInt32(id);
            bool success = await _product.DeleteProduct(proid);
            try
            {
                
                if (success)
                {
                    return RedirectToPage("PageProducts");
                }
                else
                {
                    return Page();
                }
            }
            catch
            {
                return null;
            }
        }

    }
}
