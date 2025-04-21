namespace CinemaCity.Models
{
    public class Session
    {
        public string Id { get; set; }
        public string SessionTime { get; set; }
        public string Theater { get; set; }
        public DateTime Date { get; set; }
        public string FilmId { get; set; }
        public Film Film { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }

}
