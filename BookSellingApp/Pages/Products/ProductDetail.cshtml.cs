using BusinessObject.DTOs;
using DataAccess.IRipository.IProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSellingApp.Pages.Products
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProduct _product;
        public ProductDetailModel(IProduct product) => _product = product; 
        public ProductDTO products { get; set; }
        public async Task OnGet()
        {
            try
            {
                if (HttpContext.Request.Query.TryGetValue("id", out var id))
                {
                    if (int.TryParse(id, out int productId))
                    {
                        products = await _product.GetDetail(productId);
                        
                    }
                }
            }
            catch
            {

            }
        }
    }
}
