using BiddingSystem.Domain.Commands;
using BiddingSystem.Domain.Models;
using BiddingSystem.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace BiddingSystem.Application
{
    public class PlaceBidCommandConsumer : IConsumer<PlaceBidCommand>
    {
        private readonly IBidRepository _repository;
        private readonly ILogger<PlaceBidCommandConsumer> _logger;

        public PlaceBidCommandConsumer(IBidRepository repository, ILogger<PlaceBidCommandConsumer> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PlaceBidCommand> context)
        {
            try
            {
                var command = context.Message;
                _logger.LogInformation("Processing the bid");

                var bid = new Bid
                {
                    Amount = command.Amount,
                    ItemId = command.ItemId,
                    BidderId = command.BidderId,
                    Status = BidStatus.Pending // Ensure the initial status is set to Pending
                };

                // Save the bid to the repository (ensure it is saved correctly)
                await _repository.SaveBidAsync(bid);
                _logger.LogInformation("Bid {BidId} placed", bid.Id);

                // Simulate bid processing logic
                if (command.Amount > 0)
                {
                    // Update the bid status to Accepted if the amount is greater than 0
                    bid.Status = BidStatus.Accepted; // Update the status locally first
                    await _repository.UpdateBidStatusAsync(bid.Id, bid.Status); // Then update it in the repository
                    _logger.LogInformation("Bid {BidId} accepted", bid.Id);
                }
                else
                {
                    // Reject the bid if the amount is 0 or less
                    bid.Status = BidStatus.Rejected; // Update the status locally first
                    await _repository.UpdateBidStatusAsync(bid.Id, bid.Status); // Then update it in the repository
                    _logger.LogWarning("Bid {BidId} rejected", bid.Id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing bid {BidId}", context.Message);
                throw;
            }
        }

    }
}
