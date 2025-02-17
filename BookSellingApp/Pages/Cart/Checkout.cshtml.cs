using BusinessObject.Models;
using DataAccess.IRipository.Cart;
using DataAccess.IRipository.IProducts;
using DataAccess.IRipository.IUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BookSellingApp.Pages.Cart
{
   
    public class CheckoutModel : PageModel
    {
        private readonly IOrder _order;
        private readonly IUser _user;
        public CheckoutModel(IOrder order,IUser user)
        {
            _order = order;
            _user = user;
        }
        public List<CartItem> cart = null;
        public User GetUser { get; set; }
        public decimal totalPrice = 0;
        public async Task<IActionResult> OnGet()
        {
            var userid = HttpContext.Session.GetInt32("UserID");
            var role = HttpContext.Session.GetInt32("UserRole");
            var cartJson = Request.Cookies["Cart"];
            if (role != null && cartJson !=null)
            {


                GetUser = await _user.GetUserByID(Convert.ToInt32(userid));
                if (!string.IsNullOrEmpty(cartJson))
                {
                    // Chuyển đổi JSON thành đối tượng giỏ hàng
                    cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
                    // Sử dụng biến cart ở đây
                    totalPrice = (decimal)cart.Sum(item => item.Price * item.Quantity);
                }
            }
            else
            {
                return RedirectToPage("/login/login");
            }
            return Page();
            
           
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                
                var cartJson = Request.Cookies["Cart"];
                
                if (!string.IsNullOrEmpty(cartJson))
                {
                    // Chuyển đổi JSON thành đối tượng giỏ hàng
                    cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
                    // Sử dụng biến cart ở đây
                    totalPrice = (decimal)cart.Sum(item => item.Price * item.Quantity);
                }
                var userIDType = HttpContext.Session.GetInt32("UserID");
                int userID = (int)userIDType;
                
                Order newOrder = await _order.OrderCreateAsync(userID, totalPrice);//tạo một order
                int orderID = newOrder.Id;
                foreach(var item in cart)
                {
                    OrderDetail orderDetail = await _order.OrderDetailCreateAsync(orderID, item.ProductId, item.Quantity, (decimal)item.Price);
                }
                Response.Cookies.Delete("Cart");
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi, ví dụ, hiển thị thông báo lỗi cho người dùng
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi tạo đơn hàng.");
                return Page(); // Trở lại trang checkout để người dùng thử lại
            }
        }
    }
}
