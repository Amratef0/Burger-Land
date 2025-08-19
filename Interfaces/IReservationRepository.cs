using ReservationSystem.Models;
using System.Threading.Tasks;

namespace ReservationSystem.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task AddAsync(Reservation reservation);
        Task SaveAsync();
    }
}
