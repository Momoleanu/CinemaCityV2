using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProiectIP.Controllers;
using ProiectIP.Data;
using ProiectIP.Data.Services;
using ProiectIP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectIPTest
{
    public class AdminControllerTests
    {
        private readonly DbContextOptions<AppDbContext> _options;
        private readonly IMovieObserver _observer;
        public AdminControllerTests()
        {

            _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

            var emailSettings = new EmailSettings
            {
                SmtpServer = "smtp.example.com",
                SmtpPort = 587,
                SmtpUsername = "your_username",
                SmtpPassword = "your_password",
                SenderEmail = "sender@example.com",
                SenderName = "Your Name"
            };

            // Încapsulează setările într-un obiect de tip IOptions<EmailSettings>
            var options = Options.Create(emailSettings);

            // Creează obiectul EmailService folosind constructorul
            var emailService = new EmailService(options);
            _observer = new EmailMovieObserver(emailService);
        }

    }
}
