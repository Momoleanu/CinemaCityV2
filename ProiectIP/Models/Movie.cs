using ProiectIP.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProiectIP.Models
{
    // Se mai poate modifica dupa front
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public MovieCategory MovieCategory { get; set; }


    }
}
