using ProiectIP.Models;

namespace ProiectIP.Data.Services
{
    public interface IMovieObserver
    {
        void NotifyMovieAdded(Movie movie);
        void NotifyMovieDeleted(Movie movie);
    }

}