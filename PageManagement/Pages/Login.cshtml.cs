using BusinessObject.Models;
using BusinessObject.ViewModel;
using DataAccess.IRipository.IUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PageManagement.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUser _user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginModel(IUser user)
        {
            _httpContextAccessor = new HttpContextAccessor();
            _user = user;
        }
        [BindProperty]
        public LoginViewModels input { get; set; }
        public User users { get; set; }
        public void OnGet()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserNameAD");
            _httpContextAccessor.HttpContext.Session.Remove("UserIDAD");
            _httpContextAccessor.HttpContext.Session.Remove("UserNameAD");
        }
        public IActionResult OnPost()
        {
            try
            {
                var check = _user.Login(input.Username, input.Password);
                if (check && ModelState.IsValid)
                {
                    users = _user.getUserbyName(input.Username);
                    _httpContextAccessor.HttpContext.Session.SetString("UserNameAD", input.Username);
                    _httpContextAccessor.HttpContext.Session.SetInt32("UserIDAD", users.Id);
                    _httpContextAccessor.HttpContext.Session.SetInt32("UserRoleADM", (Int32)users.IdRole);
                    return RedirectToPage("/Index");
                }
                TempData["ErrorMsg"] = "Login Failed. Check username and password!";
                return Page();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
