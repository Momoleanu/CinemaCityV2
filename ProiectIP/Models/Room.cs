using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProiectIP.Models
{
    /// <summary>
    /// Clasa model pentru o sală de proiecție.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// ID-ul sălii de proiecție.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Numele sălii de proiecție.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Descrierea sălii de proiecție.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Numărul de rânduri din sală.
        /// </summary>
        public int NoRow { get; set; }

        /// <summary>
        /// Numărul de coloane din sală.
        /// </summary>
        public int NoColumn { get; set; }

        /// <summary>
        /// Lista de filme proiectate în sală.
        /// </summary>
        public List<Movie> Movies { get; set; }
    }
}
