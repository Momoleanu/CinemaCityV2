using ProiectIP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Authentication;

namespace ProiectIP.Models
{
    /// <summary>
    /// Clasa model pentru filme.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// ID-ul filmului.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Titlul filmului.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Descrierea filmului.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Prețul filmului.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// URL-ul imaginii filmului.
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// URL-ul trailerului filmului.
        /// </summary>
        public string TrailerURL { get; set; }

        /// <summary>
        /// Ora de începere a filmului.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Ora de încheiere a filmului.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Categorie film.
        /// </summary>
        public MovieCategory MovieCategory { get; set; }

        /// <summary>
        /// Lista de actori ai filmului.
        /// </summary>
        public List<Actor_Movie> Actors_Movies { get; set; }

        /// <summary>
        /// ID-ul sălii de proiecție a filmului.
        /// </summary>
        public int RoomId { get; set; }

        /// <summary>
        /// Sala de proiecție a filmului.
        /// </summary>
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
    }
}
