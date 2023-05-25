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

namespace ProiectIP.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
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
            
            if (username == "admin" && password == "admin")
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
        [HttpPost]
        [Route("/admin/create-movie")]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {
           
            if (ModelState.IsValid)
            {
                
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();

                
                return RedirectToAction("AdminPage", "Admin");
            }

           
            return RedirectToAction("AdminPage", "Admin");
        }
        [Authorize("AdminPolicy")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            Console.WriteLine(id);
            if (ModelState.IsValid)
            {
                Movie movie = _context.Movies.Find(id);
                _context.Movies.Remove(movie);
               await _context.SaveChangesAsync();


                return RedirectToAction("Index", "Movies");
            }


            return RedirectToAction("Index", "Movies");
        }
        //Nu merge ceva la adaugare da eroare 405 cand pun return View(movie) si nu mai afiseaza nici formularul


        //Functia asta doar afiseaza formularul o sa trebuiasca stearsa
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
            //Poti sa pui return View("Login") ca sa te dea direct pe formular
            return RedirectToAction("Index","Home");
        }


    }
}
