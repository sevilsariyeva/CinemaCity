namespace CinemaCity.Models
{
    public class Author
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Biography { get; set; }
        public ICollection<Film> Films { get; set; }
    }

}
