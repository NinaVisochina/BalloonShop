using Microsoft.AspNetCore.Mvc;
using BalloonShop.Data;
using BalloonShop.Models;
using BalloonShop.Services;

namespace BalloonShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;
        private readonly ICartService _cartService;

        public CartController(ShopDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            ViewBag.Total = _cartService.GetTotal();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _cartService.AddToCart(product, quantity);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            _cartService.UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}



