namespace ProjectApp.Core.Interfaces
{
    public interface IAuctionService
    {
        List<Auction> GetAllBySeller(string userName);

        Auction GetByAuctionName(string auctionName);

        Auction GetAuctionById(int id);

        void AddAuction(Auction auction);

        List<Auction> GetAllByBidder(string userName);
        List<Auction> GetAllAuctions();
        
        void AddBid(Auction auction, Bid bid);

        void UpdateDescription(Auction auction);

        List<Auction> GetAuctionsWon(string bidder);

        int GetHighestBid(Auction auction);
    }
}
