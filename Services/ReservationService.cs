using ReservationSystem2022.Models;
using ReservationSystem2022.Repositories;

namespace ReservationSystem2022.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;


        public ReservationService(IReservationRepository repository, IUserRepository userRepository, IItemRepository itemRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
        }
    
        public async Task<ReservationDTO> CreateReservationAsync(ReservationDTO dto) //luodaan varaus
        {
            if(dto.StartTime>= dto.EndTime)
            {
                return null;
            }
            Item target = await _itemRepository.GetItemAsync(dto.Target);
            if(target == null)
            {
                return null;
            }
            IEnumerable<Reservation> reservations = await _repository.GetReservationsAsync(target, dto.StartTime, dto.EndTime);
            if(reservations.Count() > 0)
            {
                return null;
            }
            Reservation newReservation = await DTOToReservationAsync(dto);

            newReservation = await _repository.AddReservationAsync(newReservation);

            return ReservationToDTO(newReservation);
        }

        public async Task<bool> DeleteReservationAsync(long id) //poistetaan varaus
        {
            Reservation oldReservation = await _repository.GetReservationAsync(id);
            if (oldReservation == null)
            {
                return false;
            }
            return await _repository.DeleteReservationAsync(oldReservation);
        
    }

        public async Task<ReservationDTO> GetReservationAsync(long id) //haetaan varaus
        {
            Reservation reservation = await _repository.GetReservationAsync(id);


            if (reservation != null)
            {
                return ReservationToDTO(reservation);
            }
            return null;
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsAsync() //haetaan varaus
        {
        IEnumerable<Reservation> reservation = await _repository.GetReservationsAsync();
        List<ReservationDTO> result = new List<ReservationDTO>();
            foreach (Reservation i in reservation)
            {
                result.Add(ReservationToDTO(i));
            }
            return result;
        }



public Task<IEnumerable<Reservation>> GetReservationsAsync(Item target, DateTime startTime, DateTime endTime) //haetaan varaus
        {
            throw new NotImplementedException();
        }

        public async Task<ReservationDTO> UpdateReservationAsync(ReservationDTO reservation) //päivitetään varaus
        {
            Reservation oldReservation = await _repository.GetReservationAsync(reservation.Id);
            if (oldReservation == null)
            {
                return null;
            }





            if (reservation.StartTime >= reservation.EndTime)
            {
                return null;
            }
            IEnumerable<Reservation> reservations = await _repository.GetReservationsAsync(oldReservation.Target, reservation.StartTime, reservation.EndTime);
            if (reservations.Count() > 1)
            {
                return null;
            }

            if (reservations.Count() == 1 && reservations.First().Id != oldReservation.Id)
            {
                return null;
            }
        
            oldReservation.StartTime = reservation.StartTime;
            oldReservation.EndTime = reservation.EndTime;

            Reservation updatedReservation = await _repository.UpdateReservationAsync(oldReservation);
            if (updatedReservation == null)
            {
                return null;
            }

            return ReservationToDTO(updatedReservation);



        }

        private async Task<Reservation> DTOToReservationAsync(ReservationDTO dto)
        {
            Reservation res = new Reservation();
            User owner = await _userRepository.GetUserAsync(dto.Owner);
            if(owner == null)
            {
                return null;
            }
            Item target = await _itemRepository.GetItemAsync(dto.Target);
            if(target == null)
            {
                return null;
            }
            res.Id = dto.Id;
            res.Owner = owner;
            res.Target = target;
            res.StartTime= dto.StartTime;
            res.EndTime= dto.EndTime;
            
            return res;
        }

        private ReservationDTO ReservationToDTO(Reservation res)
        {
            ReservationDTO dto = new ReservationDTO();

            dto.Id = res.Id;
            dto.Target = res.Target.Id;
            dto.Owner = res.Owner.UserName;
            dto.StartTime= res.StartTime;
            dto.EndTime= res.EndTime;

            return dto;
        }
    }
}
