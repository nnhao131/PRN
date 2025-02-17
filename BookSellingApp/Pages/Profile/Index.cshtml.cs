using BusinessObject.Models;
using DataAccess.IRipository.IProducts;
using DataAccess.IRipository.IUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSellingApp.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly IUser _user;
        private readonly IOrder _order;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IndexModel(IUser user,IOrder order, IHttpContextAccessor httpContextAccessor) {
        
            _httpContextAccessor = httpContextAccessor;
            _user = user;
            _order = order;
        }
        [BindProperty]
        public ChangePass pass { get; set; }
        [BindProperty]
        public User GetUser { get;set; }
        [BindProperty]
        public List<Order> GetOrder { get; set; }
        [HttpGet]
        public async Task<IActionResult> OnGet()
        {   
            
           var user=  _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            int userID = Convert.ToInt32(user);
            if (user == null)
            {
                return RedirectToPage("/login/login");
            }
            else
            {
                var getOrderTask = _order.GetOrderByIdUser(userID);
                var getUserTask = _user.GetUserByID(userID);

                // Đợi cả hai tác vụ hoàn thành đồng thời
                await Task.WhenAll(getOrderTask, getUserTask);

                // Lấy kết quả từ cả hai tác vụ
                GetOrder = await getOrderTask;
                GetUser = await getUserTask;

            }
            return Page();

        }
        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            try
            {
                
                    var user = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
                    int userID = Convert.ToInt32(user);
                    var result = await _user.UpdateUser(GetUser,userID);


                return RedirectToPage("/Profile/Index");



            }
            catch
            {
                throw;
            }
        }
        public async Task<IActionResult> OnPostPass()
        {
            try
            {
                var user = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
                int userID = Convert.ToInt32(user);
                string oldPassword = HttpContext.Request.Form["OldPass"];
                string newPassword = HttpContext.Request.Form["NewPass"];
                string confirmPassword = HttpContext.Request.Form["CfPassword"];
                var getid = await _user.GetUserByID(userID);
                    
                if ( oldPassword==getid.Password && newPassword == confirmPassword)
                {
                    // Gọi phương thức ChChangePass để thay đổi mật khẩu
                    await _user.ChChangePass(newPassword, userID);
                }
                return RedirectToPage();
            }
            catch
            {
                throw;
            }
        }

    }
    public class ChangePass
    {
        public string OldPass { get; set;}
        public string NewPass { get; set;}
        public string CfPassword { get; set;}
    }
}
