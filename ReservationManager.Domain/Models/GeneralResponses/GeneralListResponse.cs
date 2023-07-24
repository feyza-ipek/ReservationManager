namespace ReservationManager.Domain.Models.GeneralResponses
{
    public class GeneralListResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IList<T> Items { get; set; }
    }
}
