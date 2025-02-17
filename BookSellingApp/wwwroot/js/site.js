// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(".add-to-cart").click(function () {
        var productId = $(this).data("product-id");

        $.ajax({
            url: "/Cart/AddToCart",
            data: { productId: productId, quantity: 1 },
            success: function (data) {
                alert("Product added to cart: " + data); // Thông báo khi sản phẩm được thêm vào giỏ hàng
            }
        });
    });
});
