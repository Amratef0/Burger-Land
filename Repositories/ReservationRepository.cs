using ReservationSystem.Data;
using ReservationSystem.Models;
using ReservationSystem.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ReservationSystem.Repositories.Implementations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
