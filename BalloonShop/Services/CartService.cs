using BalloonShop.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BalloonShop.Services
{
    public interface ICartService
    {
        void AddToCart(Product product, int quantity);
        void RemoveFromCart(int productId);
        List<CartItem> GetCart();
        void ClearCart();
        void UpdateQuantity(int productId, int quantity);
        decimal GetTotal();
    }

    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartSessionKey = "CartSession";

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddToCart(Product product, int quantity)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem { Product = product, ProductId = product.ProductId, Quantity = quantity });
            }
            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(p => p.ProductId == productId);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCart(cart);
            }
        }

        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }
            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
        }

        public void ClearCart()
        {
            SaveCart(new List<CartItem>());
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var cartItem = cart.FirstOrDefault(p => p.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                SaveCart(cart);
            }
        }

        public decimal GetTotal()
        {
            var cart = GetCart();
            return cart.Sum(p => p.Product.Price * p.Quantity);
        }

        private void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cartJson = JsonConvert.SerializeObject(cart);
            session.SetString(CartSessionKey, cartJson);
        }
    }
}


