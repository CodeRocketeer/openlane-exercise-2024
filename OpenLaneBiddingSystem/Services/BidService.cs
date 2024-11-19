using Microsoft.EntityFrameworkCore;
using OpenLaneBiddingSystem.Data;
using OpenLaneBiddingSystem.Models;

namespace OpenLaneBiddingSystem.Services
{
    public class BidService
    {
        private readonly AppDbContext _dbContext;

        public BidService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveBidAsync(BidRequest bidRequest)
        {
            var bid = new Bid
            {
                UserId = bidRequest.UserId,
                Amount = bidRequest.Amount
            };

            await _dbContext.Bids.AddAsync(bid);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Bid>> GetAllBidsAsync()
        {
            return await _dbContext.Bids.ToListAsync();
        }
    }
}
