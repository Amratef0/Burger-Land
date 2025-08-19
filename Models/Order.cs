using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime? OrderDate { get; set; } = DateTime.Now;

        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0.")]
        public decimal? TotalAmount { get; set; }

        [StringLength(100, ErrorMessage = "Customer name must be less than 100 characters.")]
        public string? CustomerName { get; set; }

        [StringLength(255, ErrorMessage = "Address must be less than 255 characters.")]
        public string? Address { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
