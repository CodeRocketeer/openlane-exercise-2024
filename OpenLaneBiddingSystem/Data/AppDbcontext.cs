using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace OpenLaneBiddingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Bid> Bids { get; set; }
    }
}
