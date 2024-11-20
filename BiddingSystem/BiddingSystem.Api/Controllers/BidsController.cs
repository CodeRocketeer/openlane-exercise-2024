using Microsoft.AspNetCore.Mvc;
using MassTransit;
using BiddingSystem.Domain.Commands;

namespace BiddingSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public BidsController(IPublishEndpoint publishEndpoint, ILogger<BidsController> logger)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceBid(PlaceBidCommand command)
        {
            // Publish the bid command
            await _publishEndpoint.Publish(command);
            return Accepted(new { command.ItemId, command.BidderId });
        }
    }
}
