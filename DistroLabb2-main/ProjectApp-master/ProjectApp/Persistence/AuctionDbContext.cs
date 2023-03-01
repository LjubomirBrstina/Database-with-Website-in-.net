using Microsoft.EntityFrameworkCore;
using ProjectApp.Core;
using ProjectApp.ViewModels;

namespace ProjectApp.Persistence
{
    public class AuctionDbContext : DbContext
    {
        public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }

        public DbSet<BidDb> BidDbs { get; set; }
        public DbSet<AuctionDb> AuctionDbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AuctionDb adb = new AuctionDb()
            {
                Id = -1,
                AuctionName = "Bok", // from seed data
                Description = "Lär dig läsa",
                Seller = "ljubomir@kth.se",
                StartingPrice = 100,
                ExpireDate = DateTime.Now.AddHours(5),
                BidDbs = new List<BidDb>()
            };
            modelBuilder.Entity<AuctionDb>().HasData(adb);

            BidDb bdb1 = new BidDb()
            {
                Id = -1,
                Bidder = "Joel@kth.se",
                Amount = 120,
                TimeSubmitted = DateTime.Now,
                AuctionId = -1,
            };

            BidDb bdb2 = new BidDb()
            {
                Id = -2,
                Bidder = "Ljubo@kth.se",
                Amount = 125,
                TimeSubmitted = DateTime.Now,
                AuctionId = -1,
            };

            modelBuilder.Entity<BidDb>().HasData(bdb1);
            modelBuilder.Entity<BidDb>().HasData(bdb2);
        }

        public DbSet<ProjectApp.Core.Auction> Auction { get; set; }

        public DbSet<ProjectApp.ViewModels.AuctionVM> AuctionVM { get; set; }
    }
}
