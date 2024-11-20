using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BiddingSystem.Domain.Commands
{
    public record PlaceBidCommand
    {
        public required decimal Amount { get; init; }
        public required string ItemId { get; init; }
        public required string BidderId { get; init; }
    }
}
