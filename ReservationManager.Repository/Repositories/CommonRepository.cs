using ReservationManager.Persistence.DataContext;
using ReservationManager.Repository.IRepositories;

namespace ReservationManager.Repository.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ReservationManagerDbContext _reservationManagerDbContext;
        public CommonRepository(ReservationManagerDbContext reservationManagerDbContext)
        {
            _reservationManagerDbContext = reservationManagerDbContext;
        }

       

        public void SaveChanges()
        {
            _reservationManagerDbContext.SaveChanges();
        }
    }
}
