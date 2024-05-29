using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BalloonShop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        [Required]
        public string? Type { get; set; }

        [Required]
        public string? Form { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        public int QuantityInPack { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public string Model { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }

        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
