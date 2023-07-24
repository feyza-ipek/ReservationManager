using ReservationManager.Domain.Models.EmailSender;

namespace ReservationManager.Infrastructure.Email.Interfaces
{
    public interface IEmailSender
    {
        bool Send(EmailSenderDto sendDto);
    }
}
