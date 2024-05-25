using Microsoft.AspNetCore.Mvc;
using BalloonShop.Data;
using BalloonShop.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BalloonShop.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminPanelController(ShopDbContext context)
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

            return View(products.ToList()); // Перетворення на список перед передачею до представлення
        }

        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name");
            ViewData["SubCategories"] = new SelectList(_context.SubCategories, "SubCategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
             _context.Products.Add(product);
             _context.SaveChanges();
             return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["SubCategories"] = new SelectList(_context.SubCategories, "SubCategoryId", "Name");
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
             _context.Categories.Add(category);
             _context.SaveChanges();
             return RedirectToAction(nameof(Index));
            
            //return View(category);
        }

        public IActionResult CreateSubCategory()
        {

            ViewData["Categories"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateSubCategory(SubCategory subCategory)
        {
                _context.SubCategories.Add(subCategory);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            
           
        }
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

