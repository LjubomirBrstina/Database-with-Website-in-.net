using ProjectApp.Core.Interfaces;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ProjectApp.Core
{
    public class AuctionService : IAuctionService
    {
        private IAuctionPersistence _auctionPersitence;

        public AuctionService(IAuctionPersistence auctionPersitence)
        {
            _auctionPersitence = auctionPersitence;
        }   

        public List<Auction> GetAllBySeller(string userName)
        {
            return _auctionPersitence.GetAllBySeller(userName);
        }

        public Auction GetByAuctionName(string auctionName)
        {
            return _auctionPersitence.GetByAuctionName(auctionName);
        }

        public Auction GetAuctionById(int id)
        {
            return _auctionPersitence.GetAuctionById(id);
        }

        public void AddAuction(Auction auction)
        {
            // assume no tasks in new project
            if (auction == null) throw new InvalidDataException();
            //auction.CreatedDate = DateTime.Now; KANSKE LÄGGA IsExpired() istället
            _auctionPersitence.Add(auction);  
            //auction.AddBid;
        }

        public List<Auction> GetAllByBidder(string userName)
        {
            return _auctionPersitence.GetAllByBidder(userName);
        }

        public List<Auction> GetAllAuctions()
        {
            return _auctionPersitence.GetAllAuctions();
        }

        public void AddBid(Auction auction, Bid bid)
        {
            if (bid == null) throw new InvalidDataException();
            _auctionPersitence.AddBid(auction, bid);
        }

        public void UpdateDescription(Auction auction)
        {
            if (auction == null) throw new InvalidDataException();
            _auctionPersitence.UpdateDescription(auction);
        }
        public List<Auction> GetAuctionsWon(string bidder)
        {
            if (bidder == null) throw new InvalidDataException();
            return _auctionPersitence.GetAuctionsWon(bidder);
        }
        public int GetHighestBid(Auction auction)
        {
            if (auction == null) throw new InvalidDataException();
            return _auctionPersitence.GetHighestBid(auction);
        }
    }
}
