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
        public IActionResult Index(int? subCategoryId, string[] manufacturers, string[] sizes)
        {
            var subcategories = new SelectList(_context.SubCategories.ToList(), nameof(SubCategory.SubCategoryId), nameof(SubCategory.Name));
            ViewBag.Categories = subcategories;

            var products = _context.Products.Include(p => p.SubCategory).AsQueryable();

            if (subCategoryId.HasValue)
            {
                products = products.Where(p => p.SubCategoryId == subCategoryId.Value);
            }

            if (manufacturers != null && manufacturers.Length > 0)
            {
                products = products.Where(p => manufacturers.Contains(p.Manufacturer));
            }

            if (sizes != null && sizes.Length > 0)
            {
                products = products.Where(p => sizes.Contains(p.Size));
            }

            var manufacturerList = _context.Products
                .Select(p => p.Manufacturer)
                .Distinct()
                .ToList();

            var sizeList = _context.Products
                .Select(p => p.Size)
                .Distinct()
                .ToList();

            ViewBag.Manufacturers = manufacturerList;
            ViewBag.Sizes = sizeList;

            return View(products.ToList());
        }
        public IActionResult ProductsBySubCategory(int subCategoryId)
        {
            var subcategories = new SelectList(_context.SubCategories.ToList(), nameof(SubCategory.SubCategoryId), nameof(SubCategory.Name));
            ViewBag.Categories = subcategories;

            var products = _context.Products
                                   .Include(p => p.SubCategory)
                                   .Where(p => p.SubCategoryId == subCategoryId)
                                   .ToList();
            var manufacturerList = _context.Products
                .Select(p => p.Manufacturer)
                .Distinct()
                .ToList();

            var sizeList = _context.Products
                .Select(p => p.Size)
                .Distinct()
                .ToList();

            ViewBag.Manufacturers = manufacturerList;
            ViewBag.Sizes = sizeList;

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
        public IActionResult Search(string query)
        {
            var products = _context.Products
                                   .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                                   .ToList();

            return View("Index", products);
        }
    }
}
