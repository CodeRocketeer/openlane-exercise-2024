using BiddingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BiddingSystem.Infrastructure.Repositories
{
    public interface IBidRepository
    {
        Task SaveBidAsync(Bid bid);
        Task<Bid?> GetBidAsync(Guid id);
        Task UpdateBidStatusAsync(Guid id, BidStatus status);
    }
}
