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


    [Column("total_price", TypeName = "decimal(18,2)")]
    public decimal? TotalPrice { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    [InverseProperty("Basket")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("UserId")]
    [InverseProperty("Baskets")]
    public virtual User? User { get; set; }
}
