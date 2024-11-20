using BiddingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiddingSystem.Infrastructure.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly BiddingDbContext _context;

        public BidRepository(BiddingDbContext context)
        {
            _context = context;
        }

        public async Task SaveBidAsync(Bid bid)
        {
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
        }

        public async Task<Bid?> GetBidAsync(Guid id)
        {
            return await _context.Bids.FindAsync(id);
        }

        public async Task UpdateBidStatusAsync(Guid id, BidStatus status)
        {
            var bid = await _context.Bids.FindAsync(id);
            if (bid != null)
            {
                bid.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }

}
