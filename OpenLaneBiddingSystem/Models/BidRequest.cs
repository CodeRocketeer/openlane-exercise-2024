using System.ComponentModel.DataAnnotations;

namespace OpenLaneBiddingSystem.Models
{
    public class BidRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bid amount must be greater than zero.")]
        public decimal Amount { get; set; }
    }
}
