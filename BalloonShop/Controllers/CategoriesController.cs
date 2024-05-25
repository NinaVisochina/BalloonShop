using BalloonShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BalloonShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ShopDbContext _context;

        public CategoriesController(ShopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.Include(c => c.SubCategories).ThenInclude(sc => sc.Products).ToList();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = _context.Categories
                .Include(c => c.SubCategories)
                .ThenInclude(sc => sc.Products)
                .FirstOrDefault(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
    }
}
