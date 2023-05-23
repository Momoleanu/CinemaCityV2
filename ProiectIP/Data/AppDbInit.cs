using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using ProiectIP.Models;
using System;

namespace ProiectIP.Data
{
    public class AppDbInit
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if(!context.Rooms.Any())
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
                    }
                        );
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
                            Bio  = "bfdfnggfmnfghmgfm"
                        },
                        new Actor()
                        {
                            FullName = "Eugen Neagoe",
                            PictureURL = "url",
                            Bio  = "bfdfnggfmnfghmgfm"
                        },
                        new Actor()
                        {
                            FullName = "Eugen Neagoe",
                            PictureURL = "url",
                            Bio  = "bfdfnggfmnfghmgfm"
                        },
                        new Actor()
                        {
                            FullName = "Eugen Neagoe",
                            PictureURL = "url",
                            Bio  = "bfdfnggfmnfghmgfm"
                        }

                    }
                        );
                    context.SaveChanges();
                }
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Title = "Ana are mere",
                            Description = "Ana are pere",
                            Price = "30",
                            ImageURL = "url",
                            StartTime = DateTime.Now.AddDays(-10),
                            EndTime = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.Action,
                            RoomId = 3
                        },
                        new Movie()
                        {
                            Title = "Ana are mere",
                            Description = "Ana are pere",
                            Price = "30",
                            ImageURL = "url",
                            StartTime = DateTime.Now.AddDays(-10),
                            EndTime = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.Drama,
                            RoomId = 1
                        },
                        new Movie()
                        {
                            Title = "Ana are mere",
                            Description = "Ana are pere",
                            Price = "30",
                            ImageURL = "url",
                            StartTime = DateTime.Now.AddDays(-10),
                            EndTime = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.Comedy,
                            RoomId = 1
                        },
                        new Movie()
                        {
                            Title = "Ana are mere",
                            Description = "Ana are pere",
                            Price = "30",
                            ImageURL = "url",
                            StartTime = DateTime.Now.AddDays(-10),
                            EndTime = DateTime.Now.AddDays(-2),
                            MovieCategory = MovieCategory.SF,
                            RoomId = 1
                        }
                    }
                        );
                    context.SaveChanges();
                }
                if (!context.Actors_Movies.Any())
                {
                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 7
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 8
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
