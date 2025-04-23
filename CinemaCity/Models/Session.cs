using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("sessions")]
public partial class Session
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("sessionTime")]
    [StringLength(255)]
    public string? SessionTime { get; set; }

    [Column("theater")]
    [StringLength(255)]
    public string? Theater { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [Column("film_id")]
    public int? FilmId { get; set; }

    [ForeignKey("FilmId")]
    [InverseProperty("Sessions")]
    public virtual Film? Film { get; set; }

    [InverseProperty("Session")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
