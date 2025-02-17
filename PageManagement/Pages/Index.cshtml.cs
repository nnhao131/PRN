using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PageManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel()
        {
            
        }

        public async Task<IActionResult> OnGet()
        {
            var userrole = HttpContext.Session.GetInt32("UserRoleADM");
            if (Convert.ToInt32(userrole) != 1)
            {

                return RedirectToPage("/Login");
            }
            return Page();
        }
    }
}