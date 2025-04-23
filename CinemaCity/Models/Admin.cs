using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Models;

[Table("admin")]
public partial class Admin
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
}
