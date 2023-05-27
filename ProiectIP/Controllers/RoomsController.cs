using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectIP.Data;
using System.Threading.Tasks;

namespace ProiectIP.Controllers
{
    /// <summary>
    /// Controller pentru gestionarea acțiunilor legate de camere.
    /// </summary>
    public class RoomsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor pentru RoomsController.
        /// </summary>
        /// <param name="context">Contextul bazei de date.</param>
        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Acțiune pentru afișarea tuturor camerelor.
        /// </summary>
        /// <returns>Vizualizarea listei de camere.</returns>
        public async Task<IActionResult> Index()
        {
            var allRooms = await _context.Rooms.ToListAsync();

            return View(allRooms);
        }
    }
}
