using AgileApps.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgileApps.Controllers
{
    public class CartController : Controller
    {
        private static List<CartItem> cart = new List<CartItem>();

        public IActionResult Index()
        {
            return View(cart);
        }

       

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var item = cart.FirstOrDefault(p => p.ProductId == id);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var item = cart.FirstOrDefault(p => p.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            return RedirectToAction("Index");
        }
        public IActionResult AddToCart(int id, string name, decimal price)
        {
            var product = ProductController.products.FirstOrDefault(p => p.Id == id);

            if (product == null || product.Stock <= 0)
            {
                TempData["Error"] = "Sản phẩm đã hết hàng!";
                return RedirectToAction("Index", "Product");
            }

            var item = cart.FirstOrDefault(p => p.ProductId == id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = id,
                    ProductName = name,
                    Price = price,
                    Quantity = 1
                });
            }

            product.Stock--;

            TempData["Success"] = "Đã thêm vào giỏ hàng!";
            return RedirectToAction("Index", "Product");
        }


        public IActionResult Checkout()
        {
            // Xử lý thanh toán tại đây
            cart.Clear();
            return RedirectToAction("Index");
        }
    }
}
