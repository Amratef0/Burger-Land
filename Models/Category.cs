using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, ErrorMessage = "The Name must be less than 100 characters.")]
        public string? Name { get; set; }

        public virtual ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
