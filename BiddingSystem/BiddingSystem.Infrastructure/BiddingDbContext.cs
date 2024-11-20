using Microsoft.EntityFrameworkCore;
using BiddingSystem.Domain.Models;

namespace BiddingSystem.Infrastructure
{
    public class BiddingDbContext : DbContext
    {
        public BiddingDbContext(DbContextOptions<BiddingDbContext> options) : base(options)
        {
        }

        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bid>(entity =>
            {
                // Set up primary key
                entity.HasKey(b => b.Id);

                // Configure Amount to be stored as a double
                entity.Property(b => b.Amount).HasConversion<double>();

                // Configure BidStatus to be stored as an integer (Enum)
                entity.Property(b => b.Status).HasConversion<int>();

                // Configure ItemId and BidderId to be required
                entity.Property(b => b.ItemId).IsRequired();
                entity.Property(b => b.BidderId).IsRequired();

                // Indexes for frequently queried fields
                entity.HasIndex(b => b.ItemId).HasDatabaseName("IX_Bid_ItemId");
                entity.HasIndex(b => b.BidderId).HasDatabaseName("IX_Bid_BidderId");

                // Configure Timestamp with default value
                entity.Property(b => b.Timestamp)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP")  // or CURRENT_TIMESTAMP based on your DB
                      .ValueGeneratedOnAdd();
            });
        }
    }
}
