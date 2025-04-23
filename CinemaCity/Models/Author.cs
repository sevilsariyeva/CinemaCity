using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("authors")]
public partial class Author
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fullName")]
    [StringLength(255)]
    public string? FullName { get; set; }

    [Column("biography")]
    [StringLength(255)]
    public string? Biography { get; set; }

    [InverseProperty("Author")]
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
