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
            //mymodel.Actors = from a in _context.Actors join am in _context.Actors_Movies
                             //on a.Id equals am.ActorId join m in _context.Movies 
              //on m.Id equals am.MovieId
            return View(mymodel);
        }
    }
}
