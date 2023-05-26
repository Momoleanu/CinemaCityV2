using ProiectIP.Models;

namespace ProiectIP.Data.Services
{
    public interface IMovieObserver
    {
        void NotifyMovieAdded(Movie movie);
        void NotifyMovieDeleted(Movie movie);

        void Subscribe(string email);
        void Unsubscribe(string email);
    }

}