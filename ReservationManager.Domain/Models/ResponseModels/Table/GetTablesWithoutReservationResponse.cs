
namespace ReservationManager.Domain.Models.ResponseModels.Table
{
    public class GetTablesWithoutReservationResponse
    {
        public int TableId { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public bool Cancel { get; set; }
    }
}
