using BusinessObject.Models;
using BusinessObject.ViewModel;
using DataAccess.IRipository.IUser;
using DataAccess.Ripository.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSellingApp.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUser _userRepository;
        public LoginModel(IUser userRepo, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public LoginViewModels input { get; set; }
        public User user { get; set; }
        public IActionResult OnGet()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserName");
            _httpContextAccessor.HttpContext.Session.Remove("UserID");
            _httpContextAccessor.HttpContext.Session.Remove("UserRole");
            return Page();
        }
        public IActionResult OnPost()
        {
            try
            {
                var check = _userRepository.Login(input.Username, input.Password);
                if (check && ModelState.IsValid)
                {
                    user = _userRepository.getUserbyName(input.Username);
                    _httpContextAccessor.HttpContext.Session.SetString("UserName", input.Username);
                    _httpContextAccessor.HttpContext.Session.SetInt32("UserID", user.Id);
                    _httpContextAccessor.HttpContext.Session.SetInt32("UserRole", (Int32)user.IdRole);
                    return RedirectToPage("/Products/Index");
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
