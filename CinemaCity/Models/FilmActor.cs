using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("film_actors")]
public partial class FilmActor
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("film_id")]
    public int? FilmId { get; set; }

    [Column("actor_id")]
    public int? ActorId { get; set; }

    [ForeignKey("ActorId")]
    [InverseProperty("FilmActors")]
    public virtual Actor? Actor { get; set; }

    [ForeignKey("FilmId")]
    [InverseProperty("FilmActors")]
    public virtual Film? Film { get; set; }
}
