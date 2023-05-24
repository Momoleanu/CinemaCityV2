using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectIP.Data;
using ProiectIP.Models;
using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectIP.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _context.Movies.ToListAsync();
            return View(allMovies);
        }

        public  IActionResult Movie(int id)
        {
            Console.WriteLine(id);
            dynamic mymodel = new ExpandoObject();
            mymodel.Movie =  _context.Movies.Where(x => x.Id == id).FirstOrDefault();
            mymodel.Actors = _context.Actors_Movies
                         .Where(movieActor => movieActor.MovieId == id)
                         .Join(
                            _context.Actors,
                            movieActor => movieActor.ActorId,
                            actor => actor.Id,
                            (movieActor, actor) => actor
                            )
                         .ToList();
            return View(mymodel);
        }
    }
}
