using BalloonShop.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BalloonShop.Services
{
    public interface ICartService
    {
        void AddToCart(Product product);
        void RemoveFromCart(int productId);
        List<Product> GetCart();
        void ClearCart();
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartSession";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToCart(Product product)
        {
            var cart = GetCart();
            cart.Add(product);
            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var product = cart.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                cart.Remove(product);
                SaveCart(cart);
            }
        }

        public List<Product> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<Product>();
            }
            return JsonConvert.DeserializeObject<List<Product>>(cartJson);
        }

        public void ClearCart()
        {
            SaveCart(new List<Product>());
        }

        private void SaveCart(List<Product> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(CartSessionKey, cartJson);
        }
    }
}

