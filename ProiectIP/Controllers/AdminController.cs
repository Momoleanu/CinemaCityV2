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
 *  Clasa Controller pentru Admin                                         *
 *                                                                        *
 **************************************************************************/

using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using ProiectIP.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ProiectIP.Models;
using ProiectIP.Data.Services;
using System.IO;

namespace ProiectIP.Controllers
{
    /// <summary>
    /// Controller responsabil de gestionarea acțiunilor și logicii specifice pentru administrator.
    /// </summary>
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMovieObserver _movieObserver;

        /// <summary>
        /// Constructor controller Admin
        /// </summary>
 
        public AdminController(AppDbContext context, IMovieObserver movieObserver)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _movieObserver = movieObserver ?? throw new ArgumentNullException(nameof(movieObserver));
        }

        /// <summary>
        /// Returnează pagina de autentificare a administratorului.
        /// </summary>
        /// <param name="returnUrl">URL-ul către care să se facă redirect după autentificare.</param>
        /// <returns>View-ul paginii de autentificare.</returns>
        [HttpGet]
        [Route("/admin/login")]
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Procesează cererea de autentificare a administratorului.
        /// </summary>
        /// <param name="username">Numele de utilizator introdus.</param>
        /// <param name="password">Parola introdusă.</param>
        /// <param name="returnUrl">URL-ul către care să se facă redirect după autentificare.</param>
        /// <returns>Rezultatul acțiunii.</returns>
        [HttpPost]
        [Route("/admin/login")]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = "")
        {
            string[] admin = System.IO.File.ReadAllLines("Data\\admin.txt");
            if (username == admin[0] && BCrypt.Net.BCrypt.Verify(password, admin[1]))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "AdminScheme");

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    IsPersistent = true
                };

                var principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(principal, authProperties);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("AdminPage");
            }

            ViewBag.ErrorMessage = "Nume de utilizator sau parolă incorecte.";
            return View();
        }

        /// <summary>
        /// Returnează pagina principală pentru administrator, în funcție de starea de autentificare.
        /// </summary>
        /// <returns>View-ul paginii principale pentru administrator sau a paginii de autentificare.</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("/admin")]
        public IActionResult Index()
        {
            if (Request.Cookies["admin"] != null && Request.Cookies["admin"] == "true")
            {
                return View("AdminPage");
            }

            return View("Login");
        }

        /// <summary>
        /// Returnează pagina de administrare pentru administrator.
        /// </summary>
        /// <returns>View-ul paginii de administrare.</returns>
        [HttpGet]
        [Authorize("AdminPolicy")]
        [Route("/admin/adminpage")]
        public IActionResult AdminPage()
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "admin")
            {
                ViewBag.CanCreateMovies = true;
            }
            else
            {
                ViewBag.CanCreateMovies = false;
            }

            dynamic mymodel = new ExpandoObject();
            mymodel.Movies = _context.Movies.ToList();
            mymodel.Actors = _context.Actors.ToList();

            return View(mymodel);
        }

        /// <summary>
        /// Procesează cererea de creare a unui nou film.
        /// </summary>
        /// <param name="movie">Obiectul de tip Movie care reprezintă filmul de creat.</param>
        /// <param name="movieObserver">Serviciul pentru observarea filmelor.</param>
        /// <returns>Rezultatul acțiunii.</returns>
        [Authorize("AdminPolicy")]
        [Route("/admin/create-movie")]
        [HttpPost]
        public async Task<IActionResult> CreateMovie(Movie movie, [FromServices] IMovieObserver movieObserver)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();

                movieObserver.NotifyMovieAdded(movie);

                return RedirectToAction("AdminPage", "Admin");
            }

            return RedirectToAction("AdminPage", "Admin");
        }

        /// <summary>
        /// Procesează cererea de ștergere a unui film.
        /// </summary>
        /// <param name="id">ID-ul filmului de șters.</param>
        /// <param name="movieObserver">Serviciul pentru observarea filmelor.</param>
        /// <returns>Rezultatul acțiunii.</returns>
        [Authorize("AdminPolicy")]
        public async Task<IActionResult> DeleteMovie(int id, [FromServices] IMovieObserver movieObserver)
        {
            Console.WriteLine(id);
            if (ModelState.IsValid)
            {
                Movie movie = _context.Movies.Find(id);
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                movieObserver.NotifyMovieDeleted(movie);

                return RedirectToAction("Index", "Movies");
            }

            return RedirectToAction("Index", "Movies");
        }

        /// <summary>
        /// Returnează pagina de creare a unui nou film pentru administrator.
        /// </summary>
        /// <returns>View-ul paginii de creare a unui nou film.</returns>
        [HttpGet]
        [Authorize("AdminPolicy")]
        [Route("/admin/create-movie")]
        public IActionResult GetCreateMovie()
        {
            return View("CreateMovie");
        }

        /// <summary>
        /// Procesează cererea de delogare a administratorului.
        /// </summary>
        /// <returns>Rezultatul acțiunii.</returns>
        [HttpGet]
        [Route("/admin/logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("AdminScheme");
            Response.Cookies.Delete("AdminScheme");

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Route("/admin/subscribers")]
        public  IActionResult Subscribers()
        {
            var subscribers =  _movieObserver.Subscribers;
            return View(subscribers);
        }
        [HttpGet]
        [Route("/admin/delete-subscribers")]
        public IActionResult DeleteSubscribers()
        {
            using (StreamWriter writer = new StreamWriter("Data\\subscribers.txt"))
            {
                writer.Write("");
            }
            return RedirectToAction("Subscribers", "Admin");
        }
    }
}
