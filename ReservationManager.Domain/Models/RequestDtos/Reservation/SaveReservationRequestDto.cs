namespace ReservationManager.Domain.Models.RequestDtos.Reservation
{
    public class SaveReservationRequestDto
    {
        public string CustomerName { get; set; }
        public DateTime? ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int TableNumber { get; set; }
    }
}
