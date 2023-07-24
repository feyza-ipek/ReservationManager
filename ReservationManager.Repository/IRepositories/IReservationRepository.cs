using ReservationManager.Domain.Entities;

namespace ReservationManager.Repository.IRepositories
{
    public interface IReservationRepository
    {
        int SaveReservation(Reservation entitySaveReservation);
        void DeleteReservation(Reservation entityDeleteReservation);
        Reservation GetById(int id);
    }
}
