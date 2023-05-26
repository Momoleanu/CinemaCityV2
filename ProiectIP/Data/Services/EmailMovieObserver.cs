using ProiectIP.Data.Services;
using ProiectIP.Models;
using System;
using System.Collections.Generic;

public class EmailMovieObserver : IMovieObserver
{
    private readonly IEmailService _emailService;

    public EmailMovieObserver(IEmailService emailService)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public void NotifyMovieAdded(Movie movie)
    {
        
        List<string> subscribers = GetSubscribers(); 

        string subject = "Notificare: Film adăugat";
        string body = $"Filmul '{movie.Title}' cu prețul '{movie.Price}' a fost adăugat cu succes.";

        foreach (string subscriber in subscribers)
        {
            _emailService.SendEmailAsync(subscriber, subject, body).Wait();
        }
    }

    public void NotifyMovieDeleted(Movie movie)
    {
     
        List<string> subscribers = GetSubscribers(); 

        string subject = "Notificare: Film șters";
        string body = $"Implementare Observator pe o lista de subscrieberi. Filmul '{movie.Title}' a fost șters.";

        foreach (string subscriber in subscribers)
        {
            _emailService.SendEmailAsync(subscriber, subject, body).Wait();
        }
    }

    private List<string> GetSubscribers()
    {
        

        List<string> subscribers = new List<string>
        {
            "mihneaholban1@gmail.com",
            "blacksmecher123@gmail.com"
        };

        return subscribers;
    }
}