using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("users")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fullName")]
    [StringLength(255)]
    public string? FullName { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("password")]
    [StringLength(255)]
    public string? Password { get; set; }

    [Column("phone")]
    [StringLength(255)]
    public string? Phone { get; set; }

    [Column("imageUrl")]
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();

    [InverseProperty("User")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
