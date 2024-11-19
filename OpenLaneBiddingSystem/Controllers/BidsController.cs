using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenLaneBiddingSystem.Data;
using OpenLaneBiddingSystem.Models;
using OpenLaneBiddingSystem.Services;

namespace OpenLaneBiddingSystem.Controllers
{
    [ApiController]
    [Route("api/bids")]
    public class BidsController : ControllerBase
    {
        private readonly BidService _bidService;

        // Inject the BidService directly.
        public BidsController(BidService bidService)
        {
            _bidService = bidService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitBid([FromBody] BidRequest bidRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Save the bid directly using BidService.
            try
            {
                await _bidService.SaveBidAsync(bidRequest);
                return Accepted();  // Return a 202 Accepted status code after the bid is saved.
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while saving the bid: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBids()
        {
            // Fetch all bids directly from the database using BidService.
            var bids = await _bidService.GetAllBidsAsync();
            return Ok(bids);  // Return all the bids.
        }
    }
}
