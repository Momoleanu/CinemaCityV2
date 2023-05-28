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
 *  Clasa Controller pentru acasa                                         *
 *                                                                        *
 **************************************************************************/



using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProiectIP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectIP.Controllers
{
    /// <summary>
    /// Controller pentru pagina principală și acțiunile asociate.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor pentru HomeController.
        /// </summary>
        /// <param name="logger">Logger utilizat pentru înregistrarea mesajelor de log.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Acțiune pentru afișarea paginii principale.
        /// </summary>
        /// <returns>Vizualizarea paginii principale.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Acțiune pentru afișarea paginii de confidențialitate.
        /// </summary>
        /// <returns>Vizualizarea paginii de confidențialitate.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Acțiune pentru afișarea paginii de eroare.
        /// </summary>
        /// <returns>Vizualizarea paginii de eroare.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
