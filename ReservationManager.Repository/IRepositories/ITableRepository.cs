using ReservationManager.Domain.Entities;
using ReservationManager.Domain.Models.RequestDtos.Table;

namespace ReservationManager.Repository.IRepositories
{
    public interface ITableRepository
    {
        int SaveTable(SaveTableRequestDto requestSaveTable);
        List<Table> GetTableList();
        List<Table> GetTablesWithoutReservation(GetTablesWithoutReservationRequestDto requestGetTablesWithoutReservation);
    }
}
