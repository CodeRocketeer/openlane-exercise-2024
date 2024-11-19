using System.ComponentModel.DataAnnotations;

namespace OpenLaneBiddingSystem.Data
{
    public class Bid
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
