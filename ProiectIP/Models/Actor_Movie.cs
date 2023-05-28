namespace ProiectIP.Models
{
    /// <summary>
    /// Model pentru asocierea între actori și filme.
    /// </summary>
    public class Actor_Movie
    {
        /// <summary>
        /// ID-ul filmului.
        /// </summary>
        public int MovieId { get; set; }

        /// <summary>
        /// Filmul asociat.
        /// </summary>
        public Movie Movie { get; set; }

        /// <summary>
        /// ID-ul actorului.
        /// </summary>
        public int ActorId { get; set; }

        /// <summary>
        /// Actorul asociat.
        /// </summary>
        public Actor Actor { get; set; }
    }
}
