using BusinessObject.Models;
using DataAccess.IRipository.IUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PageManagement.Pages
{
    public class PageAccountModel : PageModel
    {
        private readonly IUser _user;
        public PageAccountModel(IUser user)
        {
            _user = user;
        }
        public List<User> users { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var userrole = HttpContext.Session.GetInt32("UserRoleADM");
            if (Convert.ToInt32(userrole) != 1)
            {

                return RedirectToPage("/Login");
            }
            users = _user.GetAll();
            return Page();
        }
        [HttpPost]
        public async Task<IActionResult> OnPostLock(int id)
        {
            try
            {
                
                var useiR= await _user.LockUser(id);
                return new JsonResult(useiR);
            }
            catch
            {
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUnLock(int id)
        {
            try
            {
                var userR = await _user.LockUnLockUser(id);
                return new JsonResult(userR);
            }
            catch
            {
                throw;
            }
        }
    }
}
