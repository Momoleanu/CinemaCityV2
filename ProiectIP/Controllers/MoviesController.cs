/**************************************************************************
 *                                                                        *
 *  Description: Sistem de rezervari bilete cinema                        *
 *  Website:     https://github.com/Momoleanu/ProiectIP                   *
 *  Copyright:   (c) 2023, Holban Mihnea                                  *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 *  Clasa Controller pentru Filme                                         *
 *                                                                        *
 **************************************************************************/


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectIP.Data;
using ProiectIP.Data.Services;
using ProiectIP.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectIP.Controllers
{
    /// <summary>
    /// Controller pentru gestionarea acțiunilor legate de filme.
    /// </summary>
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMovieObserver _movieObserver;

        /// <summary>
        /// Constructor pentru MoviesController.
        /// </summary>
        /// <param name="context">Contextul bazei de date.</param>
        /// <param name="movieObserver">Obiectul observator de filme.</param>
        public MoviesController(AppDbContext context, IMovieObserver movieObserver)
        {
            _context = context;
            _movieObserver = movieObserver;
        }

        /// <summary>
        /// Acțiune pentru afișarea tuturor filmelor.
        /// </summary>
        /// <returns>Vizualizarea listei de filme.</returns>
        public async Task<IActionResult> Index()
        {
            var allMovies = await _context.Movies.ToListAsync();
            return View(allMovies);
        }

        /// <summary>
        /// Acțiune pentru afișarea detaliilor despre un film specific.
        /// </summary>
        /// <param name="id">ID-ul filmului.</param>
        /// <returns>Vizualizarea detaliilor despre film.</returns>
        public IActionResult Movie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
                return NotFound();

            var actors = _context.Actors_Movies
                .Where(movieActor => movieActor.MovieId == id)
                .Join(
                    _context.Actors,
                    movieActor => movieActor.ActorId,
                    actor => actor.Id,
                    (movieActor, actor) => actor
                )
                .ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.Movie = movie;
            mymodel.Actors = actors;

            return View(mymodel);
        }

        /// <summary>
        /// Acțiune pentru achiziționarea unui bilet pentru un film.
        /// </summary>
        /// <param name="title">Titlul filmului.</param>
        /// <param name="price">Prețul biletului.</param>
        /// <param name="quantity">Cantitatea de bilete achiziționate.</param>
        /// <param name="email">Adresa de email a utilizatorului.</param>
        /// <param name="subscribe">Indicator pentru abonarea la notificări.</param>
        /// <returns>Redirecționarea către acțiunea de confirmare a achiziției.</returns>
        [HttpPost]
        public IActionResult Buy(string title, string price, int quantity, string email, bool subscribe)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Title == title);
            Console.WriteLine(quantity);
            if (quantity <= 0)
            {
                return Redirect($"/Movies/Movie/{movie.Id}");
            }
            if (movie == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(email) && subscribe)
            {
                _movieObserver.Subscribe(email);
            }

            return RedirectToAction("Confirmation", new { movieId = movie.Id, quantity = quantity });
        }

        /// <summary>
        /// Acțiune pentru afișarea paginii de confirmare a achiziției.
        /// </summary>
        /// <param name="movieId">ID-ul filmului achiziționat.</param>
        /// <param name="quantity">Cantitatea de bilete achiziționate.</param>
        /// <returns>Vizualizarea paginii de confirmare.</returns>
        public IActionResult Confirmation(int movieId, int quantity)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);

            ViewData["MovieTitle"] = movie?.Title;
            ViewData["Quantity"] = quantity;

            return View();
        }

        /// <summary>
        /// Acțiune pentru afișarea paginii de succes a achiziției.
        /// </summary>
        /// <returns>Vizualizarea paginii de succes.</returns>
        public IActionResult Success()
        {
            return View();
        }
    }
}
