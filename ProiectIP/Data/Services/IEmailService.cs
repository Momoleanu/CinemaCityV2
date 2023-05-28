/**************************************************************************
 *                                                                        *
 *  Description: Sistem de rezervari bilete cinema                        *
 *  Website:     https://github.com/Momoleanu/ProiectIP                   *
 *  Copyright:   (c) 2023, Holban Mihnea, Dumitru Andrei                  *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *                     
 *                                                                        *
 **************************************************************************/


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
