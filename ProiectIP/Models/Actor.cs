using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProiectIP.Models
{
    /// <summary>
    /// Modelul pentru un actor.
    /// </summary>
    public class Actor
    {
        /// <summary>
        /// Identificatorul unic al actorului.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// URL-ul imaginii actorului.
        /// </summary>
        public string PictureURL { get; set; }

        /// <summary>
        /// Numele complet al actorului.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Biografia actorului.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Lista de asociere între actori și filme.
        /// </summary>
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
