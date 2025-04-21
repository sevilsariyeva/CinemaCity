namespace CinemaCity.Models
{
    public class Actor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Film> Films { get; set; }
    }

}
