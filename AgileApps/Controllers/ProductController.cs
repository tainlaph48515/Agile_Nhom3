using Microsoft.AspNetCore.Mvc;
using AgileApps.Models;

namespace AgileApps.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "iPhone 15", Price = 25000000, Category = "Điện thoại", Stock = 10 },
        new Product { Id = 2, Name = "MacBook Air M2", Price = 30000000, Category = "Laptop", Stock = 5 }
    };

        public IActionResult Index(string search)
        {
            var result = string.IsNullOrEmpty(search) ? products :
                         products.Where(p => p.Name.Contains(search)).ToList();
            return View(result);
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
