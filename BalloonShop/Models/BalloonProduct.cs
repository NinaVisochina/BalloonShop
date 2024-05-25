using System.ComponentModel.DataAnnotations;

namespace BalloonShop.Models
{
    public class BalloonProduct : Product
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Form { get; set; }
    }
}
