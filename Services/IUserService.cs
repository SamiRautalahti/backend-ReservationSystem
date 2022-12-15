using ReservationSystem2022.Models;

namespace ReservationSystem2022.Services
{
    public interface IUserService
    {
        public Task<Models.UserDTO> CreateUserAsync(User user);
    }
}
