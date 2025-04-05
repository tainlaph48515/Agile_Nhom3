using Microsoft.AspNetCore.Mvc;
using AgileApps.Models;

namespace AgileApps.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> products = new List<Product>
{
    new Product {
        Id = 1, Name = "iPhone 15", Price = 25000000, Category = "Điện thoại",
        Stock = 10, Color = "Đen", Description = "Điện thoại cao cấp mới nhất của Apple.",
        ImageUrl = "/images/iphone15.jpg"
    },
    new Product {
        Id = 2, Name = "MacBook Air M2", Price = 30000000, Category = "Laptop",
        Stock = 5, Color = "Bạc", Description = "Laptop siêu nhẹ chạy chip Apple M2.",
        ImageUrl = "/images/macbook.jpg"
    }
};


        public IActionResult Index(string search, string color, decimal? minPrice, decimal? maxPrice)
        {
            var result = products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                result = result.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(color))
                result = result.Where(p => p.Color != null && p.Color.Contains(color, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                result = result.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                result = result.Where(p => p.Price <= maxPrice.Value);

            return View(result.ToList());
        }
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }


        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var existing = products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Category = product.Category;
                existing.Stock = product.Stock;
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null) products.Remove(product);
            return RedirectToAction("Index");
        }
    }
}
