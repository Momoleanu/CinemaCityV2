using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using ProiectIP.Models;

namespace ProiectIP.Data
{
    /// <summary>
    /// Clasa AppDbInit este responsabilă de inițializarea bazei de date cu date de test.
    /// </summary>
    public class AppDbInit
    {
        /// <summary>
        /// Metoda Seed este utilizată pentru a adăuga date de test în baza de date.
        /// </summary>
        /// <param name="applicationBuilder">Obiectul IApplicationBuilder pentru configurarea aplicației.</param>
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(new List<Room>()
                    {
                        new Room()
                        {
                            Name = "A1",
                            Description = "A1",
                            NoRow = 6,
                            NoColumn = 6
                        },
                        new Room()
                        {
                            Name = "A2",
                            Description = "A2",
                            NoRow = 4,
                            NoColumn = 6
                        },
                        new Room()
                        {
                            Name = "A3",
                            Description = "A3",
                            NoRow = 6,
                            NoColumn = 7
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Eugen Neagoe",
                            PictureURL = "url",
                            Bio = "bfdfnggfmnfghmgfm"
                        },
                        new Actor()
                        {
                            FullName = "Eugen Neagoe",
                            PictureURL = "url",
                            Bio = "bfdfnggfmnfghmgfm"
                        },
                        new Actor()
                        {
                            FullName = "Eugen Neagoe",
                            PictureURL = "url",
                            Bio = "bfdfnggfmnfghmgfm"
                        },
                        new Actor()
                        {
                            FullName = "Eugen Neagoe",
                            PictureURL = "url",
                            Bio = "bfdfnggfmnfghmgfm"
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Title = "KANDAHAR: MISIUNE MORTALA",
                            Description = "După ce a sabotează un reactor nuclear iranian, agentul CIA sub acoperire Tom Harris (Gerard Butler) descoperă că fotografia sa a fost răspândită în toată presa, când un turnător dezvăluie implicarea lui în distrugerea reactorului. Dar reporterul care a primit informațiile despre Harris este arestat de Garda Revoluționară a Iranului, așa că Harris are la dispoziție 30 de ore pentru a găsi o cale de scapare.",
                            Price = "30",
                            ImageURL = "https://www.cinemacity.ro/xmedia-cw/repo/feats/posters/5742S2R-lg.jpg",
                            StartTime = DateTime.Now.AddDays(-10),
                            EndTime = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.Action,
                            RoomId = 3
                        },
                        new Movie()
                        {
                            Title = "BAUBAUL",
                            Description = "Încă suferind după moartea recentă a mamei lor, o adolescentă și sora ei mai mică încep să fie terorizate în propria casă de o prezență sadică. Lipsite de ajutor, cele două fete trebuie să-și scoată tatăl din letargia provocată de pierderea suferită de familia lor înainte de-a fi prea târziu.",
                            Price = "25",
                            ImageURL = "https://www.cinemacity.ro/xmedia-cw/repo/feats/posters/5659S2R-lg.jpg",
                            StartTime = DateTime.Now.AddDays(-3),
                            EndTime = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.Horror,
                            RoomId = 1
                        },
                        new Movie()
                        {
                            Title = "OMUL-PAIANJEN: PRIN LUMEA PAIANJENULUI",
                            Description = "Miles Morales revine pentru următorul capitol din saga Spider-Verse, câștigătoare a premiului Oscar. O aventură epică în care prietenosul Om Păianjen își va uni forțele cu Gwen Stacy și o nouă echipă de Oameni Păianjeni pentru a se confrunta cu un răufăcător mai puternic decât orice au întâlnit vreodată.",
                            Price = "30",
                            ImageURL = "https://www.cinemacity.ro/xmedia-cw/repo/feats/posters/4923S2R-lg.jpg",
                            StartTime = DateTime.Now.AddDays(-4),
                            EndTime = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.SF,
                            RoomId = 1
                        },
                        new Movie()
                        {
                            Title = "FLASH",
                            Description = "Barry Allen/Flash își folosește super-viteza pentru a schimba trecutul, dar încercarea lui de a-și salva familia creează o lume fără supereroi, forțându-l să pornească într-o misiune pentru a salva viitorul.",
                            Price = "30",
                            ImageURL = "https://www.cinemacity.ro/xmedia-cw/repo/feats/posters/5637S2R-lg.jpg",
                            StartTime = DateTime.Now.AddDays(-7),
                            EndTime = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.SF,
                            RoomId = 1
                        }
                    });
                    context.SaveChanges();
                }

                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 2
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 3
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
