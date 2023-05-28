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
 *  Clasa Controller pentru Actori                                        *
 *                                                                        *
 **************************************************************************/


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectIP.Data;
using ProiectIP.Data.Services;
using ProiectIP.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectIP.Controllers
{
    /// <summary>
    /// Controller pentru gestionarea acțiunilor legate de actori.
    /// </summary>
    public class ActorsController : Controller
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructorul clasei ActorsController.
        /// </summary>
        /// <param name="context">Contextul bazei de date.</param>
        public ActorsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Acțiunea pentru afișarea detaliilor unui actor.
        /// </summary>
        /// <param name="id">ID-ul actorului.</param>
        /// <returns>View-ul cu detaliile actorului.</returns>
        public IActionResult Actor(int id)
        {
            var data = _context.Actors.Where(x => x.Id == id).FirstOrDefault();
            return View(data);
        }
    }
}
