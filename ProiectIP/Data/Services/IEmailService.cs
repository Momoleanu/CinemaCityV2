using System.Threading.Tasks;

namespace ProiectIP.Data.Services
{
    /// <summary>
    /// Interfața IEmailService definește contractul pentru serviciul de trimitere a e-mailurilor.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Metoda SendEmailAsync trimite un e-mail către o adresă specificată.
        /// </summary>
        /// <param name="email">Adresa de e-mail destinatară.</param>
        /// <param name="subject">Subiectul e-mailului.</param>
        /// <param name="message">Mesajul e-mailului.</param>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
