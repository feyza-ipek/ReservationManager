using Microsoft.EntityFrameworkCore;
using ReservationManager.Domain.Entities;
using ReservationManager.Domain.Models.RequestDtos.Table;
using ReservationManager.Persistence.DataContext;
using ReservationManager.Repository.IRepositories;

namespace ReservationManager.Repository.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly ReservationManagerDbContext _reservationManagerDbContext;
        public TableRepository(ReservationManagerDbContext reservationManagerDbContext)
        {
            _reservationManagerDbContext = reservationManagerDbContext;
        }
        
        public int SaveTable(SaveTableRequestDto requestSaveTable)
        {
            var entity = new Table()
            {
                Number = requestSaveTable.Number,
                Capacity = requestSaveTable.Capacity,
            };
            _reservationManagerDbContext.Tables.Add(entity);
            return entity.Id;

        }
        public List<Table> GetTableList()
        {
            return _reservationManagerDbContext.Tables.AsNoTracking().ToList();
        }

        public List<Table> GetTablesWithoutReservation(GetTablesWithoutReservationRequestDto requestGetTablesWithoutReservation)
        {
            var tableList = _reservationManagerDbContext.Tables.ToList();
            return _reservationManagerDbContext.Tables.Where(x => x.Capacity >= requestGetTablesWithoutReservation.Guests)
                .GroupJoin(_reservationManagerDbContext.Reservations.Where(x => x.ReservationDate.Date == requestGetTablesWithoutReservation.Date), table => table.Id, reservation => reservation.TableNumber, (table, reservation) => new { table, reservation })
                .Select(x => x.table)
                .ToList();
        }
    }
}
