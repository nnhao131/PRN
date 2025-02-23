using DataAccess.IRipository.IProducts;
using DataAccess.IRipository.IUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PageManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrder _order;
        private readonly IProduct _product;
        private readonly IUser _user;

        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalUsers { get; set; }
        public int TotalProducts { get; set; }
        public int NewOrdersThisWeek { get; set; }
        public decimal RevenueThisWeek { get; set; }

        public IndexModel(IOrder order, IProduct product, IUser user)
        {
            _order = order;
            _product = product;
            _user = user;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            const int RECENT_DAY = 7;
            TotalOrders = await _order.GetTotalOrder();
            TotalRevenue = await _order.GetTotalRevenue();
            NewOrdersThisWeek = await _order.GetOrdersInDay(RECENT_DAY);
            RevenueThisWeek = await _order.GetRevenueInDay(RECENT_DAY);
            TotalProducts = await _product.GetTotalProduct();
            TotalUsers = await _user.GetTotalUser();

            var userrole = HttpContext.Session.GetInt32("UserRoleADM");
            if (Convert.ToInt32(userrole) != 1)
            {

                return RedirectToPage("/Login");
            }
            return Page();
        }


    }
}