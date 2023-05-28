using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProiectIP.Controllers;
using ProiectIP.Data;
using ProiectIP.Data.Services;
using ProiectIP.Models;
using System;
using System.Threading.Tasks;

namespace ProiectIP
{
    /// <summary>
    /// Clasa de configurare a aplicației.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructorul clasei Startup.
        /// </summary>
        /// <param name="configuration">Obiectul de configurare al aplicației.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Obiectul de configurare al aplicației.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Metoda apelată de runtime pentru adăugarea serviciilor în container.
        /// </summary>
        /// <param name="services">Containerul de servicii.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Adaugare db context config
            // Constructor sql
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
            services.AddControllersWithViews();
            services.AddScoped<IMovieObserver, EmailMovieObserver>();

            // Configurare Servicii

            services.AddTransient<MoviesController>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();

            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication("AdminScheme")
                .AddCookie("AdminScheme", options =>
                {
                    options.LoginPath = "/admin/login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                    options.Events = new CookieAuthenticationEvents
                    {
                        OnSigningOut = context =>
                        {
                            context.Response.Cookies.Delete("AdminScheme");
                            return Task.CompletedTask;
                        }
                    };
                    options.Cookie.IsEssential = true;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }

        /// <summary>
        /// Metoda apelată de runtime pentru configurarea pipeline-ului de procesare al cererilor HTTP.
        /// </summary>
        /// <param name="app">Obiectul de configurare a aplicației.</param>
        /// <param name="env">Obiectul de mediu de găzduire al aplicației.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseCookiePolicy();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "admin-login",
                    pattern: "admin/login",
                    defaults: new { controller = "Admin", action = "Login" }
                );

                endpoints.MapControllerRoute(
                    name: "admin-logout",
                    pattern: "admin/logout",
                    defaults: new { controller = "Admin", action = "Logout" }
                );

                endpoints.MapControllerRoute(
                    name: "movies",
                    pattern: "movies/{action}/{id?}",
                    defaults: new { controller = "Movies", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "buy-ticket",
                    pattern: "movies/buy/{title}/{price}/{quantity}",
                    defaults: new { controller = "Movies", action = "Buy" }
                );
                endpoints.MapControllerRoute(
                    name: "help-page",
                    pattern: "helpPage",
                    defaults: new { controller = "HelpPage", action = "Index" }
);

            });


            AppDbInit.Seed(app);
        }
    }
}
