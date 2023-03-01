using ProjectApp.Core;

namespace ProjectApp.ViewModels
{
    public class AuctionDetailsVM
    {
        public string Seller { get; set; }

        public string AuctionName { get; set; }

        public string Description { get; set; }

        public int StartingPrice { get; set; }

        public bool IsExpired { get; set; }

        public List<BidVM> BidVMs { get; set; } = new();

        public static AuctionDetailsVM FromAuction(Auction auction)
        {
            var detailsVM = new AuctionDetailsVM()
            {
                Seller = auction.Seller,
                Description = auction.Description,
                StartingPrice = auction.StartingPrice,
                AuctionName = auction.AuctionName,
                IsExpired = auction.IsExpired()
            };
            foreach(var bid in auction._bids)
            {
                detailsVM.BidVMs.Add(BidVM.FromBid(bid));
            }

            return detailsVM;
        }
    }
}
