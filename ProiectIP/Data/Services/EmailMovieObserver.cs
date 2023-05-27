using ProiectIP.Data.Services;
using ProiectIP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using File = System.IO.File;

public class EmailMovieObserver : IMovieObserver
{
    private List<string> subscribers;
    private readonly IEmailService _emailService;

    public EmailMovieObserver(IEmailService emailService)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        subscribers = new List<string>();
        ReadFileEmail();
    }

    public void Subscribe(string newEmail)
    {
        using (StreamWriter sw = File.AppendText("C:\\Users\\Dumitru Andrei\\Source\\Repos\\ProiectIP\\ProiectIP\\Data\\subscribers.txt"))
        {
            sw.WriteLine(newEmail);
            subscribers.Add(newEmail);
        }

    }
    public void Unsubscribe(string email)
    {
        subscribers.Remove(email);
    }
    public void NotifyMovieAdded(Movie movie)
    {
        try
        {
            ReadFileEmail();
            string subject = "Notificare: Film adăugat";
            string body = $"Filmul '{movie.Title}' cu prețul '{movie.Price}' a fost adăugat cu succes.";

            foreach (string subscriber in subscribers)
            {
                _emailService.SendEmailAsync(subscriber, subject, body).Wait();
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void NotifyMovieDeleted(Movie movie)
    {
        try
        {
            ReadFileEmail();
            string subject = "Notificare: Film șters";
            string body = $"Implementare Observator pe o lista de subscrieberi. Filmul '{movie.Title}' a fost șters.";

            foreach (string subscriber in subscribers)
            {
                _emailService.SendEmailAsync(subscriber, subject, body).Wait();
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

   public List<string> Subscribers
    {
        get
        {
            return subscribers;
        }
    }

    private void ReadFileEmail()
    {
        string[] buffer = File.ReadAllLines("C:\\Users\\mihne\\Desktop\\Programare\\ProiectIP\\ProiectIP\\Data\\subscribers.txt");
        for (int i = 0; i < buffer.Length; i++)
        {
            subscribers.Add(buffer[i]);
        }
    }
}