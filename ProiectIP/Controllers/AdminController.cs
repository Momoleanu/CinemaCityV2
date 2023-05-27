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
using Microsoft.EntityFrameworkCore;
using ProiectIP.Data.Services;

namespace ProiectIP.Controllers
{
    public class AdminController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IMovieObserver _movieObserver;

        public AdminController(AppDbContext context, IMovieObserver movieObserver)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _movieObserver = movieObserver ?? throw new ArgumentNullException(nameof(movieObserver));
        }

        [HttpGet]
        [Route("/admin/login")]
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("/admin/login")]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = "")
        {
            string[] admin = System.IO.File.ReadAllLines("C:\\Users\\Dumitru Andrei\\Source\\Repos\\ProiectIP\\ProiectIP\\Data\\admin.txt");
            Console.WriteLine(BCrypt.Net.BCrypt.HashPassword("admin"));
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
     
        [HttpGet]
        [Authorize("AdminPolicy")]
        [Route("/admin/create-movie")]
        public IActionResult GetCreateMovie()
        {

            return View("CreateMovie");
        }

        [HttpGet]
        [Route("/admin/logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("AdminScheme");
            Response.Cookies.Delete("AdminScheme");
           
            return RedirectToAction("Index", "Home");
        }


    }
}