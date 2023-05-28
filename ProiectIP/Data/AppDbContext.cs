/**************************************************************************
 *                                                                        *
 *  Description: Sistem de rezervari bilete cinema                        *
 *  Website:     https://github.com/Momoleanu/ProiectIP                   *
 *  Copyright:   (c) 2023, Dumitru Andrei                                 *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *                      
 *                                                                        *
 **************************************************************************/


using Microsoft.EntityFrameworkCore;
using ProiectIP.Models;

namespace ProiectIP.Data
{
    /// <summary>
    /// Clasa AppDbContext reprezintă contextul de bază al aplicației pentru accesul la baza de date.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor vid 
        /// </summary>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Metoda OnModelCreating este utilizată pentru configurarea relațiilor între tabele în baza de date.
        /// </summary>
        /// <param name="modelBuilder">Obiectul ModelBuilder pentru configurarea modelului bazei de date.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor_Movie>().HasKey(am => new
            {
                am.ActorId,
                am.MovieId
            });

            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Movie).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<Actor_Movie>().HasOne(m => m.Actor).WithMany(am => am.Actors_Movies).HasForeignKey(m => m.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
