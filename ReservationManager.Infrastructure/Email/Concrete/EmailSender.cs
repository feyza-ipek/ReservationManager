using ReservationManager.Domain.Models.ConfigurationModels.Interfaces;
using ReservationManager.Domain.Models.EmailSender;
using ReservationManager.Infrastructure.Email.Interfaces;
using System.Net.Mail;

namespace ReservationManager.Infrastructure.Email.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly ISmtpConfigurationModel _smtpConfigurationModel;
        public EmailSender(ISmtpConfigurationModel smtpConfigurationModel)
        {
            _smtpConfigurationModel = smtpConfigurationModel;
        }
        /// <summary>
        /// İlgili Method Test Amaçlı Return True Dönüşü Sağlanmıştır.
        /// </summary>
        /// <param name="sendDto"></param>
        /// <returns></returns>
        public bool Send(EmailSenderDto sendDto)
        {
            using SmtpClient smtpClient = new(_smtpConfigurationModel.Host, _smtpConfigurationModel.Port);
            try
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(_smtpConfigurationModel.MailAddress,
                    _smtpConfigurationModel.Password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = _smtpConfigurationModel.Ssl;
                var mail = new MailMessage()
                {
                    From = new MailAddress(_smtpConfigurationModel.MailAddress, "İş Sistem")
                };
                mail.To.Add(new MailAddress(sendDto.EmailAddress));
                mail.Body = sendDto.Body;
                mail.Subject = sendDto.Subject;
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return true;
            }
            finally
            {
                smtpClient.Dispose();
            }

        }
    }
}
