using BalloonShop.Data;
using BalloonShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BalloonShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopDbContext _context;

        public ProductsController(ShopDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? subCategoryId)
        {
            var subcategories = new SelectList(_context.SubCategories.ToList(), nameof(SubCategory.SubCategoryId), nameof(SubCategory.Name));
            ViewBag.Categories = subcategories;

            var products = _context.Products.Include(p => p.SubCategory).AsQueryable();

            if (subCategoryId.HasValue)
            {
                products = products.Where(p => p.SubCategoryId == subCategoryId.Value);
            }
            return View(products.ToList()); 
        }
        public IActionResult ProductsBySubCategory(int subCategoryId)
        {
            var products = _context.Products
                                   .Include(p => p.SubCategory)
                                   .Where(p => p.SubCategoryId == subCategoryId)
                                   .ToList();


            return View("Index", products);
        }
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
