using BusinessObject.Models;
using DataAccess.DAO.Product;
using DataAccess.IRipository.IProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PageManagement.Pages
{
    public class PageCategoryModel : PageModel
    {
        private readonly ICategoryRepository _cate;
        public PageCategoryModel(ICategoryRepository cate)=>_cate = cate;
        public List<Category> category {  get; set; }
      
        public async Task<IActionResult> OnGet()
        {
            var userrole = HttpContext.Session.GetInt32("UserRoleADM");
            if (Convert.ToInt32(userrole) != 1)
            {

                return RedirectToPage("/Login");
            }
            category = await _cate.GetAllCate();
            return Page();
        }
      //  [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> OnPost()
        {

            string catename = Request.Form["Catename"];
            string Description = Request.Form["Description"];
            var create = new Category
            {

                CategoryName = catename,
                Description = Description

            };
            var createCate = await _cate.CreateCategory(create);
            return new JsonResult(createCate);
        }
        [HttpGet]
        public async Task<IActionResult> OnGetDelete(int id)
        {
            var model = await _cate.GetCateByID(id);

            return new JsonResult(new { model });
        }
        [HttpPost]
        public async Task<IActionResult> OnPostDelete(int id)
        {
            string ids = Request.Form["myId"];
           
            bool success = await _cate.DeleteCategoryt(id);
            try
            {

                if (success)
                {
                    return RedirectToPage("PageProducts");
                }
                else
                {
                    return Page();
                }
            }
            catch
            {
                return null;
            }
        }
        [HttpGet]
        public async Task<IActionResult> OnGetUpdate(int id)
        {
            var model = await _cate.GetCateByID(id);

            return new JsonResult(new { model });
        }
        [HttpPost]
        public async Task<IActionResult> OnPostUpdate()
        {
            string ids = Request.Form["cateID"];
            string catename = Request.Form["cateName"];
            string cateDes = Request.Form["description"];
            try
            {
                var cate = new Category
                {
                    Id = Convert.ToInt32(ids),
                    CategoryName = catename,
                    Description= cateDes,
                };
                var Update = _cate.UpdateCategory(cate);

                return RedirectToPage("PageCategory");
            }
            catch
            {
                throw;
            }
        }

    }
}
