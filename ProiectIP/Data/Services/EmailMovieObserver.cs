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
 *  Clasa EmailMovieObserver implementează interfața IMovieObserver și    *
 *  gestionează abonarea, dezabonarea și notificarea observatorilor       *
 *  pentru evenimente legate de filme prin e-mail.                        *
 *                                                                        *
 **************************************************************************/


using ProiectIP.Data.Services;
using ProiectIP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using File = System.IO.File;

/// <summary>
/// Clasa EmailMovieObserver implementează interfața IMovieObserver și gestionează abonarea, dezabonarea și notificarea observatorilor pentru evenimente legate de filme prin e-mail.
/// </summary>
public class EmailMovieObserver : IMovieObserver
{
    private List<string> subscribers;
    private readonly IEmailService _emailService;

    /// <summary>
    /// Constructorul clasei EmailMovieObserver.
    /// </summary>
    /// <param name="emailService">Serviciul de trimitere a e-mailurilor.</param>
    public EmailMovieObserver(IEmailService emailService)
    {
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        subscribers = new List<string>();
        ReadFileEmail();
    }

    /// <summary>
    /// Metoda Subscribe permite abonarea unui nou e-mail.
    /// </summary>
    /// <param name="newEmail">Adresa de e-mail care se abonează.</param>
    public void Subscribe(string newEmail)
    {
        using (StreamWriter sw = File.AppendText("Data\\subscribers.txt"))
        {
            if (!subscribers.Contains(newEmail))
            {
                sw.WriteLine(newEmail);
                subscribers.Add(newEmail);
            }
        }
    }

    /// <summary>
    /// Metoda Unsubscribe permite dezabonarea unui e-mail.
    /// </summary>
    /// <param name="email">Adresa de e-mail care se dezabonează.</param>
    public void Unsubscribe(string email)
    {
        subscribers.Remove(email);
    }

    /// <summary>
    /// Metoda NotifyMovieAdded notifică observatorii când un film nou a fost adăugat.
    /// </summary>
    /// <param name="movie">Filmul adăugat.</param>
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
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    /// <summary>
    /// Metoda NotifyMovieDeleted notifică observatorii când un film a fost șters.
    /// </summary>
    /// <param name="movie">Filmul șters.</param>
    public void NotifyMovieDeleted(Movie movie)
    {
        try
        {
            ReadFileEmail();
            string subject = "Notificare: Film șters";
            string body = $"Implementare Observator pe o listă de abonați. Filmul '{movie.Title}' a fost șters.";

            foreach (string subscriber in subscribers)
            {
                _emailService.SendEmailAsync(subscriber, subject, body).Wait();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    /// <summary>
    /// Proprietatea Subscribers returnează lista de abonați.
    /// </summary>
    public List<string> Subscribers
    {
        get
        {
            return subscribers;
        }
    }

    /// <summary>
    /// Metoda ReadFileEmail citește adresele de e-mail din fișierul "subscribers.txt" și le adaugă în lista de abonați.
    /// </summary>
    private void ReadFileEmail()
    {
        string[] buffer = File.ReadAllLines("Data\\subscribers.txt");
        for (int i = 0; i < buffer.Length; i++)
        {
            subscribers.Add(buffer[i]);
        }
    }
}
