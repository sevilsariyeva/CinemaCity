using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaCity.Models;

[Table("baskets")]
public class Basket
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("film")]
    public string? Film { get; set; }

    [Column("theater")]
    public string? Theater { get; set; }

    [Column("date")]
    public string? Date { get; set; }

    [Column("total_price", TypeName = "decimal(18,2)")]
    public decimal? TotalPrice { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("session_id")]
    public int? SessionId { get; set; }

    [ForeignKey("SessionId")]
    public virtual Session? Session { get; set; }
    [InverseProperty("Basket")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("UserId")]
    [InverseProperty("Baskets")]
    public virtual User? User { get; set; }
}
