namespace CinemaCity.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string SeatNumber { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string SessionId { get; set; }
        public Session Session { get; set; }
    }
}
