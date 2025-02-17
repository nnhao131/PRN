using BusinessObject.Helper;
using BusinessObject.ViewModel;
using DataAccess.IRipository.IUser;
using DataAccess.Ripository.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSellingApp.Pages.Login
{
    public class ForgotModel : PageModel
    {
        private readonly EmailService _emailService;
        private readonly IUser _userRepository;

        public ForgotModel(EmailService emailService, IUser userRepository)
        {
            _emailService = emailService;
            _userRepository = userRepository;
        }
        public void OnGet()
        {
        }

        [BindProperty]
        public RegisterViewModel User { get; set; }


        public IActionResult OnPost()
        {
            var checkEmail = _userRepository.getUserByEmail(User.Email);
            if (checkEmail == null)
            {
                TempData["EmailError"] = "Email is not exist.";
                return Page();
            }
            _userRepository.UpdatePass(User, _emailService);
            return RedirectToPage("./Products/Index");
        }
    }
}
