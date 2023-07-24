using ReservationManager.Domain.Entities;
using ReservationManager.Persistence.DataContext;
using ReservationManager.Repository.IRepositories;

namespace ReservationManager.Repository.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ReservationManagerDbContext _reservationManagerDbContext;
        public ReservationRepository(ReservationManagerDbContext reservationManagerDbContext)
        {
            _reservationManagerDbContext = reservationManagerDbContext;
        }
        public int SaveReservation(Reservation entitySaveReservation)
        {
           
            _reservationManagerDbContext.Reservations.Add(entitySaveReservation);
            return entitySaveReservation.Id;
        }

        public void DeleteReservation(Reservation entityDeleteReservation)
        {
            _reservationManagerDbContext.Remove(entityDeleteReservation);
        }

        public Reservation GetById(int id)
        {
            return _reservationManagerDbContext.Reservations.FirstOrDefault(x => x.Id == id);
        }
    }
}
