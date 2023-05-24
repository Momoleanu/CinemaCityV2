using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectIP.Data;
using ProiectIP.Data.Services;
using ProiectIP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectIP.Controllers
{
    public class ActorsController : Controller
    {
        private readonly AppDbContext _context;
        public ActorsController(AppDbContext context)
        {
            _context = context;
        }
        public  IActionResult Actor(int id)
        {
            var data = _context.Actors.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
    }
}
