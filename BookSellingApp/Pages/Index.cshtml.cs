using BusinessObject.DTOs;
using DataAccess.IRipository.IProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSellingApp.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IProduct _product;
        public IndexModel( IProduct product) => _product = product;
        public List<ProductDTO> products { get; set; }
        public List<ProductDTO> cate1 { get; set; }
        public List<ProductDTO> cate2 { get; set; }
        public List<ProductDTO> cate3 { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                cate1 = await _product.GetcateName("Self-Help");
                cate2 = await _product.GetcateName("Fiction");
                cate3 = await _product.GetcateName("Science");
                products = await _product.GetHotdeals(6);
                return Page();
            }
            catch
            {
                return RedirectToPage("/Error");
            }
        }
        
    }
}