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
 *  Clasa Controller pentru Salile de proiectie                           *
 *                                                                        *
 **************************************************************************/

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
