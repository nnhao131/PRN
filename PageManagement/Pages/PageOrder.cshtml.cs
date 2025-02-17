using BusinessObject.Models;
using DataAccess.IRipository.IProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PageManagement.Pages
{
    public class PageOrderModel : PageModel
    {
        private readonly IOrder _order;
        public PageOrderModel(IOrder order)
        {
            _order = order;
        }
        public List<Order> orders { get; set; }
        
        public async Task<IActionResult> OnGet()
        {
            var userrole = HttpContext.Session.GetInt32("UserRoleADM");
            if (Convert.ToInt32(userrole) != 1)
            {

                return RedirectToPage("/Login");
            }
            orders = _order.GetAllOrder();
            return Page();
        }
        [HttpGet]
        public  async Task<IActionResult> OnGetDetail(int id)
        {

            var model =await _order.GetOrderByID(id);
            return new JsonResult(model);
        }


        [HttpPost]
        public async Task<IActionResult> OnPostUpdate(int id)
        {
            var updatedOrder = await _order.UpdateOrder(id);
            if (updatedOrder != null)
            {
                
                return new JsonResult(new { success = true, message = "Cập nhật thành công" });
            }
            else
            {
                
                return new JsonResult(new { success = false, message = "Cập nhật thất bại" });
            }
        }
        
    }
}
