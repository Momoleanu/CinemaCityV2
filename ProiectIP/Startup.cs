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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Web.Http;

namespace ProiectIP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            });


            AppDbInit.Seed(app);
        }
        public static void Register(HttpConfiguration config)
        {
            // Uncomment the following to use the documentation from XML documentation file.
            config.SetDocumentationProvider(
                new XmlDocumentationProvider(
                    HttpContext.Current.Server.MapPath("~/App_Data/Documentation.xml")));

        }
    }

}
