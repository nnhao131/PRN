using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.DAO.Product;
using DataAccess.IRipository.Cart;
using DataAccess.IRipository.IProducts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BookSellingApp.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IProduct _product;

        public IndexModel(IProduct product)
        {
            _product = product;
        }
        public decimal cartTotal = 0;
        public ProductDTO products { get; set; }
        public List<ProductDTO> proview { get; set; }
        public List<CartItem> cart = null;
        [HttpGet]
        public async Task<IActionResult> OnGet()
        {

                proview = await _product.GetAllProduct();
                
                // Đọc JSON từ cookies
                var cartJson = Request.Cookies["Cart"];

                if (!string.IsNullOrEmpty(cartJson))
                {
                    // Chuyển đổi JSON thành đối tượng giỏ hàng
                    cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
                    // Sử dụng biến cart ở đây
                    cartTotal = (decimal)cart.Sum(item => item.Price * item.Quantity);
                }


            return Page();
        }
        public IActionResult OnGetCartItems()
        {
            var cartJson = Request.Cookies["Cart"];
            List<CartItem> cart = new List<CartItem>();

            if (!string.IsNullOrEmpty(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            }

            return new JsonResult(cart);
        }
        [HttpPost]
        public IActionResult OnPostUpdateFromCart(int productId, int quantity)
        {
            var cartJson = Request.Cookies["Cart"];
            List<CartItem> cart;

            if (!string.IsNullOrEmpty(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);

                // Tìm sản phẩm có ProductId tương ứng trong danh sách cart
                var productToUpdate = cart.FirstOrDefault(item => item.ProductId == productId);

                if (productToUpdate != null)
                {
                    // Cập nhật số lượng sản phẩm
                    productToUpdate.Quantity = quantity;
                }

                // Cập nhật danh sách cart sau khi thay đổi
                cartJson = JsonConvert.SerializeObject(cart);
                Response.Cookies.Append("Cart", cartJson);

               
                return RedirectToPage();
            }
            else
            {
                // Xử lý khi không tìm thấy sản phẩm
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAddToCart(int productId, int quantity)
        {
            products  = await _product.GetDetail(productId);
            if (products != null)
            {
                var cartJson = Request.Cookies["Cart"];
                List<CartItem> cart;

                if (!string.IsNullOrEmpty(cartJson))
                {
                    cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
                }
                else
                {
                    cart = new List<CartItem>();
                }

                var existingItem = cart.FirstOrDefault(item => item.ProductId == productId);
                if (existingItem != null)
                {
                    // Nếu có, tăng số lượng lên
                    existingItem.Quantity += quantity;
                }
                else
                {
                    // Nếu không có, thêm một mục mới
                    cart.Add(new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        ProductName = products.ProductName,
                        Price = products.Price,
                       
                        

                    });
                }

                var newCartJson = JsonConvert.SerializeObject(cart);

                Response.Cookies.Append("Cart", newCartJson);

                return Content(newCartJson);
            }
            else
            {
                // Xử lý khi không tìm thấy sản phẩm
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult OnPostRemoveFromCart(int productId)
        {
            // Điều này giả định rằng bạn lưu trữ giỏ hàng trong một cookie với tên "Cart"
            var cartJson = Request.Cookies["Cart"];
            List<CartItem> cart;

            if (!string.IsNullOrEmpty(cartJson))
            {
                cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
            }
            else
            {
                cart = new List<CartItem>();
            }

            // Tìm sản phẩm trong giỏ hàng và xóa nó
            var productToRemove = cart.FirstOrDefault(item => item.ProductId == productId);

            if (productToRemove != null)
            {
                cart.Remove(productToRemove);
            }

            // Cập nhật giỏ hàng trong cookie
            var newCartJson = JsonConvert.SerializeObject(cart);
            Response.Cookies.Append("Cart", newCartJson);

            return RedirectToPage(); // Chuyển hướng trở lại trang giỏ hàng sau khi xóa sản phẩm
        }

    }
}
