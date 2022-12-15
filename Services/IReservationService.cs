using ReservationSystem2022.Models;

namespace ReservationSystem2022.Services
{
    public interface IReservationService
    {
        public Task<ReservationDTO> CreateReservationAsync(ReservationDTO dto);
        public Task<ReservationDTO> GetReservationAsync(long id);
        public Task<IEnumerable<ReservationDTO>> GetReservationsAsync();
        public Task<ReservationDTO> UpdateReservationAsync(ReservationDTO reservation);
        public Task<Boolean> DeleteReservationAsync(long id);
        Task<IEnumerable<Reservation>> GetReservationsAsync(Item target, DateTime startTime, DateTime endTime);
    }
}
