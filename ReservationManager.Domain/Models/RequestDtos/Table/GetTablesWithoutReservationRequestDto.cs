namespace ReservationManager.Domain.Models.RequestDtos.Table
{
    public class GetTablesWithoutReservationRequestDto
    {
        public DateTime? Date { get; set; }
        public int Guests { get; set; }
    }
}
