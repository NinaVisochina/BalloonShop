using BalloonShop.Data;
using BalloonShop.Models;
using BalloonShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace BalloonShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ShopDbContext _context;

        public CartController(ICartService cartService, ShopDbContext context)
        {
            _cartService = cartService;
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = _cartService.GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                for (int i = 0; i < quantity; i++)
                {
                    _cartService.AddToCart(product);
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveFromCart(productId);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }
    }
}


