
namespace ProjectApp.Core
{
    public class Bid
    {
        public int Id { get; set; }

        public string Bidder { get; set; }

        public int Amount { get; set; }

        public DateTime TimeSubmitted { get; set; }

        public Bid(string bidder, int amount, DateTime timeSubmitted)
        {
            Bidder = bidder;
            Amount = amount;
            TimeSubmitted = timeSubmitted;
        }

        public override string ToString()
        {
            return $"BidId: {Id}, Bidder: {Bidder}, Amount: {Amount}, TimeSubmitted: {TimeSubmitted}";
        }
    }
}