namespace ReservationManager.Domain.Models.GeneralResponses
{
    public class GeneralDataResponse<T> where T: class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
