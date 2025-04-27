using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CinemaCity.Models
{
    [Table("basket_details")]
    public class BasketDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("basket_id")]
        [Required]
        public int BasketId { get; set; }

        [Column("movie_json")]
        public string? MovieJson { get; set; }

        [Column("session_time")]
        public string? SessionTime { get; set; }

        [Column("theater")]
        public string? Theater { get; set; }

        [Column("date")]
        public string? Date { get; set; }

        [Column("total_price", TypeName = "decimal(18,2)")]
        public decimal? TotalPrice { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
