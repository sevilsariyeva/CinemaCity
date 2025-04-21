using Docker.DotNet.Models;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;

namespace CinemaCity.Models
{
    public class Film
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public int Year { get; set; }
        public string Duration { get; set; }
        public double Rating { get; set; }
        [Precision(10, 2)]
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Banner { get; set; }
        public string Link { get; set; }
        public string AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }

}
