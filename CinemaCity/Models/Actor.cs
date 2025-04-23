using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("actors")]
public partial class Actor
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fullName")]
    [StringLength(255)]
    public string? FullName { get; set; }

    [InverseProperty("Actor")]
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();
}
