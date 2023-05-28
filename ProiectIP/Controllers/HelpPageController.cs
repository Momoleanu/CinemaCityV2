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
 *  Clasa Controller pentru HelpPage                                      *
 *                                                                        *
 **************************************************************************/



using Microsoft.AspNetCore.Mvc;

namespace ProiectIP.Controllers
{
    public class HelpPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
