﻿namespace CinemaCity.Models.DTOs
{
    public class FilmDTO
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Duration { get; set; }
            public List<string> Genres { get; set; }
    }
}
