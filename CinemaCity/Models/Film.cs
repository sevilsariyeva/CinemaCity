using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("films")]
public partial class Film
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("about")]
    [StringLength(255)]
    public string? About { get; set; }

    [Column("year")]
    public int? Year { get; set; }

    [Column("duration")]
    [StringLength(255)]
    public string? Duration { get; set; }

    [Column("rating")]
    public double? Rating { get; set; }

    [Column("price", TypeName = "money")]
    public decimal? Price { get; set; }

    [Column("imageUrl")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [Column("banner")]
    [StringLength(255)]
    public string? Banner { get; set; }

    [Column("link")]
    [StringLength(255)]
    public string? Link { get; set; }

    [Column("author_id")]
    public int? AuthorId { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Films")]
    public virtual Author? Author { get; set; }

    [InverseProperty("Film")]
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    [InverseProperty("Film")]
    public virtual ICollection<FilmGenre> FilmGenres { get; set; } = new List<FilmGenre>();

    [InverseProperty("Film")]
    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
}
