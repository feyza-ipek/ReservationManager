using Microsoft.AspNetCore.Mvc;
using ReservationManager.Domain.Models.RequestDtos.Reservation;
using ReservationManager.Service.IServices;

namespace ReservationManager.Api.Controllers
{
    [Route("Reservation")]
    public class ReservationController : BaseController
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IBaseService baseService, IReservationService reservationService) : base(baseService)
        {
            _reservationService = reservationService;

        }
        [HttpPost]
        [Route("SaveReservation")]
        public IActionResult SaveReservation([FromBody] SaveReservationRequestDto requestSaveReservation)
        {
            var responseService = _reservationService.SaveReservation(requestSaveReservation);
            return Ok(responseService);
        }
        
    }
}
