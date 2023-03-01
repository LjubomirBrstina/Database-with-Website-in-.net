namespace ProjectApp.Core.Interfaces
{
    public interface IAuctionPersistence
    {
        List<Auction> GetAllBySeller(string userName);

        Auction GetByAuctionName(string id);

        Auction GetAuctionById(int id);

        List<Auction> GetAllAuctions();

        List<Auction> GetAllByBidder(string userName);
        void Add(Auction auction);

        void AddBid(Auction auction, Bid bid);

        void UpdateDescription(Auction auction);

        List<Auction> GetAuctionsWon(string bidder);

        int GetHighestBid(Auction auction);
    }
}
