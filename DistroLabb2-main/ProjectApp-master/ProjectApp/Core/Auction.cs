namespace ProjectApp.Core
{
    public class Auction
    {
        public int Id { get; set; }
        public string AuctionName { get; set; }

        public string Description { get; set; }

        public string Seller { get; set; }

        public int StartingPrice { get; set; }

        public DateTime ExpireDate { get; set; }

        public List<Bid> _bids = new List<Bid>();

        public Auction(string auctionName, string description, string seller, int startingPrice, DateTime expireDate)
        {
            AuctionName = auctionName;
            Description = description;
            Seller = seller;
            StartingPrice = startingPrice;
            ExpireDate = expireDate;
        }

        public Auction() { }

        public void AddBid(Bid newBid)
        {
            _bids.Add(newBid);
        }

        public bool IsExpired()
        {
            if (ExpireDate <= DateTime.Now) return true;
            return false;
        }

        public override string ToString()
        {
            return $"Auction Name: {AuctionName}, Description: {Description}, Seller: {Seller}, Starting Price: {StartingPrice}, " +
                $"Expire Date: {ExpireDate}, Is expired?: {IsExpired()}, Bids: {_bids.ToString}";
        }
    }
}
