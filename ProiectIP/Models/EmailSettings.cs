namespace ProiectIP.Models
{
    /// <summary>
    /// Setările pentru e-mail.
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Serverul SMTP pentru trimiterea e-mailurilor.
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Portul SMTP pentru trimiterea e-mailurilor.
        /// </summary>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Numele de utilizator al serverului SMTP.
        /// </summary>
        public string SmtpUsername { get; set; }

        /// <summary>
        /// Parola pentru serverul SMTP.
        /// </summary>
        public string SmtpPassword { get; set; }

        /// <summary>
        /// Adresa de e-mail a expeditorului.
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Numele expeditorului.
        /// </summary>
        public string SenderName { get; set; }
    }
}
