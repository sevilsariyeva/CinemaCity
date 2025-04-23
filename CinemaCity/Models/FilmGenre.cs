using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("film_genres")]
public partial class FilmGenre
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("film_id")]
    public int? FilmId { get; set; }

    [Column("genre_id")]
    public int? GenreId { get; set; }

    [ForeignKey("FilmId")]
    [InverseProperty("FilmGenres")]
    public virtual Film? Film { get; set; }

    [ForeignKey("GenreId")]
    [InverseProperty("FilmGenres")]
    public virtual Genre? Genre { get; set; }
}
