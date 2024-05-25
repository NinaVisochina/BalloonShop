using System.ComponentModel.DataAnnotations;

namespace BalloonShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
