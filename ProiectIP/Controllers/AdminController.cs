using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using ProiectIP.Data;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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

        [HttpPost]
        [Route("/admin/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AdminScheme");
            return View("Login");
        }

    }
}
