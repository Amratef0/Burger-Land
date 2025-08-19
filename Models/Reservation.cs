using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, ErrorMessage = "Phone number must be less than 20 characters.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(150, ErrorMessage = "Email must be less than 150 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Reservation date is required.")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Guests count is required.")]
        [Range(1, 20, ErrorMessage = "عدد الأفراد يجب أن يكون بين 1 و 20")]
        public int GuestsCount { get; set; }
    }
}
