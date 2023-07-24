using ReservationManager.Domain.Models.ConfigurationModels.Interfaces;

namespace ReservationManager.Domain.Models.ConfigurationModels.Concretes
{
    public class SmtpConfigurationModel : ISmtpConfigurationModel
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }
        public bool Ssl { get; set; }
    }
}
