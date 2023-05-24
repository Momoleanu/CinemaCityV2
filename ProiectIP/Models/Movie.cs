using ProiectIP.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Authentication;

namespace ProiectIP.Models
{
    // Se mai poate modifica dupa front
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageURL { get; set; }
        public string TrailerURL { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public MovieCategory MovieCategory { get; set; }

        public List<Actor_Movie> Actors_Movies { get; set; }

        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; } 

    }
}
