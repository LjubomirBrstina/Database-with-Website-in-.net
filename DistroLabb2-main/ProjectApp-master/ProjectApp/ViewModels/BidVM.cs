using ProjectApp.Core;
using Bid = ProjectApp.Core.Bid;

namespace ProjectApp.ViewModels
{
    public class BidVM
    {
        public int amount { get; set; }

        public string bidder { get; set; }

        public DateTime timeSubmitted { get; set; }

        public static BidVM FromBid(Bid bid)
        {
            return new BidVM()
            {
                amount = bid.Amount,
                bidder = bid.Bidder,
                timeSubmitted = bid.TimeSubmitted
            };
        }
    }
}
