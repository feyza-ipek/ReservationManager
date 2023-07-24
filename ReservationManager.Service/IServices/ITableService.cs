using ReservationManager.Domain.Models.GeneralResponses;
using ReservationManager.Domain.Models.RequestDtos.Table;
using ReservationManager.Domain.Models.ResponseModels.Table;

namespace ReservationManager.Service.IServices
{
    public interface ITableService
    {
        GeneralDataResponse<SaveTableResponse> SaveTable(SaveTableRequestDto requestSaveTable);
        GeneralListResponse<GetTableListResponse> GetTableList();
        GeneralListResponse<GetTablesWithoutReservationResponse> GetTablesWithoutReservation(GetTablesWithoutReservationRequestDto requestTablesWithoutReservation);
    }
}
