using System.Threading.Tasks;

namespace ProiectIP.Data.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

}
