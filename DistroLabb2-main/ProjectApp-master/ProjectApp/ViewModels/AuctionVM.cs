using ProjectApp.Core;

namespace ProjectApp.ViewModels
{
    public class AuctionVM
    {
        public int id { get; set; }

        public string auctionName { get; set; }

        public string description { get; set; }

        public int startingPrice { get; set; }

        public string seller { get; set; }

        public bool isExpired { get; set;  }

        public static AuctionVM FromAuction(Auction auction)
        {
            return new AuctionVM()
            {
                id = auction.Id,
                auctionName = auction.AuctionName,
                description = auction.Description,
                seller = auction.Seller,
                startingPrice = auction.StartingPrice,
                isExpired = auction.IsExpired()
            };
        }
    }
}
