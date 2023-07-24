using ReservationManager.Domain.Models.GeneralResponses;
using ReservationManager.Domain.Models.RequestDtos.Reservation;
using ReservationManager.Domain.Models.ResponseModels.Reservation;

namespace ReservationManager.Service.IServices
{
    public interface IReservationService
    {
        GeneralDataResponse<SaveReservationResponse> SaveReservation(SaveReservationRequestDto requestSaveReservation);
        GeneralResponse DeleteReservation(DeleteReservationRequestDto requestDeleteReservation);
    }
}
