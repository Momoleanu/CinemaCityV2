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



namespace ProiectIP.Data.Services
{
    using ProiectIP.Models;

    /// <summary>
    /// Interfața IMovieObserver definește contractul pentru un observator de filme.
    /// </summary>
    public interface IMovieObserver
    {
        /// <summary>
        /// Metoda NotifyMovieAdded este apelată atunci când un film este adăugat.
        /// </summary>
        /// <param name="movie">Filmul adăugat.</param>
        void NotifyMovieAdded(Movie movie);

        /// <summary>
        /// Metoda NotifyMovieDeleted este apelată atunci când un film este șters.
        /// </summary>
        /// <param name="movie">Filmul șters.</param>
        void NotifyMovieDeleted(Movie movie);

        /// <summary>
        /// Metoda Subscribe abonează un utilizator la notificări pentru filme.
        /// </summary>
        /// <param name="email">Adresa de e-mail a utilizatorului.</param>
        void Subscribe(string email);

        /// <summary>
        /// Metoda Unsubscribe dezabonează un utilizator de la notificări pentru filme.
        /// </summary>
        /// <param name="email">Adresa de e-mail a utilizatorului.</param>
        void Unsubscribe(string email);
    }
}
