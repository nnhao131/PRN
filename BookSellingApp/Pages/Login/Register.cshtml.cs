using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.ViewModel;
using DataAccess.IRipository.IUser;
using BusinessObject.Models;
using DataAccess.Ripository.Login;

namespace BookSellingApp.Pages.Login
{
    public class RegisterModel : PageModel
    {
        private readonly IUser _userRepository;
        public RegisterModel()
        {
            _userRepository = new UserRepository();
        }

        public RegisterViewModel input { get; set; }
        [BindProperty]
        public RegisterViewModel user { get; set; }
        public IActionResult OnPost()
        {
            if (user != null && ModelState.IsValid)
            {
                try
                {
                    _userRepository.Register(user);
                    return RedirectToPage("Login");
                }
                catch (Exception ex)
                {
                    TempData["RegisterFail"] = "Register failed: " + ex.Message;
                }
            }
            TempData["RegisterFail"] = "Invalid data provided";
            return Page();
        }


    }
}
