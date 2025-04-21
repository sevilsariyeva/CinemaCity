namespace CinemaCity.Models
{
    public class Genre
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Film> Films { get; set; }
    }

}
