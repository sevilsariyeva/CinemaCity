using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("tickets")]
public partial class Ticket
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("seatNumber")]
    [StringLength(255)]
    public string? SeatNumber { get; set; }

    [Column("body", TypeName = "text")]
    public string? Body { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("session_id")]
    public int? SessionId { get; set; }

    [Column("basket_id")]
    public int? BasketId { get; set; }

    [ForeignKey("BasketId")]
    [InverseProperty("Tickets")]
    public virtual Basket? Basket { get; set; }

    [ForeignKey("SessionId")]
    [InverseProperty("Tickets")]
    public virtual Session? Session { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Tickets")]
    public virtual User? User { get; set; }
}
