namespace BalloonShop.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public int CountToBuy { get; set; }
        public int Discount { get; set; }
        public bool IsAvailable { get; set; }
        public string Image { get; set; }
        public Product Product { get; set; }
        public string SubCategoryName { get; set; }  
    }
}
