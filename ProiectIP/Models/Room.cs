using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProiectIP.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NoRow{ get; set; }
        public int NoColumn { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
