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
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMovieObserver _movieObserver;

        public MoviesController(AppDbContext context, IMovieObserver movieObserver)
        {
            _context = context;
            _movieObserver = movieObserver;
        }

        public async Task<IActionResult> Index()
        {
            var allMovies = await _context.Movies.ToListAsync();
            return View(allMovies);
        }

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

        [HttpPost]
        public IActionResult Buy(string title, string price, int quantity, string email, bool subscribe)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Title == title);
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

        public IActionResult Confirmation(int movieId, int quantity)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);

            ViewData["MovieTitle"] = movie?.Title;
            ViewData["Quantity"] = quantity;

            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
