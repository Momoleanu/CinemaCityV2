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
