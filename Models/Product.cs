using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, ErrorMessage = "The Name must be less than 100 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, 100000, ErrorMessage = "The Price must be greater than 0.")]
        public decimal? Price { get; set; }

        [StringLength(255, ErrorMessage = "The Image path must be less than 255 characters.")]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "The Category field is required.")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

        public DateTime? DateAdded { get; set; } = DateTime.Now;

        [Range(0, int.MaxValue, ErrorMessage = "Sales count cannot be negative.")]
        public int? SalesCount { get; set; } = 0;
    }
}
