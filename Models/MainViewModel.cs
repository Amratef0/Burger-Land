using System.Collections.Generic;

namespace ReservationSystem.Models
{
    public class MainViewModel
    {
        public List<Product>? Products { get; set; }
        public Reservation? Reservation { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
