using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BiddingSystem.Domain.Models
{
    public class Bid
    {
        [Key]
        public Guid Id { get; set; }
        public required decimal Amount { get; set; }
        public required string ItemId { get; set; }
        public required string BidderId { get; set; }
        public DateTime Timestamp { get; set; }
        public BidStatus Status { get; set; }
    }
}
