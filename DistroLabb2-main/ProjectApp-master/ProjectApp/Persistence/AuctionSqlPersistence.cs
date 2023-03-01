using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ProjectApp.Core;
using ProjectApp.Core.Interfaces;

namespace ProjectApp.Persistence
{
    public class AuctionSqlPersistence : IAuctionPersistence
    {
        private AuctionDbContext _dbContext;
        private IMapper _mapper;

        public AuctionSqlPersistence(AuctionDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //Hämtar alla bids till en auction
        public Auction GetByAuctionName(string auctionName)
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Include(p => p.BidDbs)
                .Where(p => p.AuctionName.Equals(auctionName)) 
                .SingleOrDefault();

            Auction auction = _mapper.Map<Auction>(auctionDbs);
            if(auctionDbs != null){
                foreach (BidDb pdb in auctionDbs.BidDbs)
                {
                    auction.AddBid(_mapper.Map<Bid>(pdb));
                }
            }
            return auction;
        }

        public int GetHighestBid(Auction auction)
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .Where(a => a.AuctionName.Equals(auction.AuctionName))
                .SingleOrDefault();

            int tmpAmount = 0;

            Auction auction2 = _mapper.Map<Auction>(auctionDbs);
            if (auctionDbs != null)
            {
                foreach (BidDb adb in auctionDbs.BidDbs)
                {

                    auction.AddBid(_mapper.Map<Bid>(adb));
                }
                foreach (Bid bid in auction._bids)
                {
                    if (bid.Amount > tmpAmount)
                    {
                        tmpAmount = bid.Amount;
                    }
                }
            }
            return tmpAmount;
        }

        public List<Auction> GetAuctionsWon(string bidder)
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .ToList();

            string tmpBidder = "";
            int tmpAmount = 0;

            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDbs)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                if (auctionDbs != null)
                {
                    foreach (BidDb bdb in adb.BidDbs)
                    {
                        auction.AddBid(_mapper.Map<Bid>(bdb));
                    }
                }
                foreach (Bid bid in auction._bids)
                {
                    if (bid.Amount > tmpAmount)
                    {
                        tmpBidder = bid.Bidder;
                        tmpAmount = bid.Amount;
                    }
                }
                if(tmpBidder.Equals(bidder) && auction.IsExpired())
                {
                    result.Add(auction);
                }
                tmpBidder = "";
                tmpAmount = 0;
            }
            return result;
        }

        //Hämtar alla bids till en auction
        public Auction GetAuctionById(int id)
        {
            var auctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .Where(a => a.Id.Equals(id))
                .SingleOrDefault();

            Auction auction = _mapper.Map<Auction>(auctionDbs);
            if (auctionDbs != null)
            {
                foreach (BidDb adb in auctionDbs.BidDbs)
                {
                    auction.AddBid(_mapper.Map<Bid>(adb));
                }
            }
            return auction;
        }

        public void Add(Auction auction)
        {
            AuctionDb adb = _mapper.Map<AuctionDb>(auction);
            _dbContext.AuctionDbs.Add(adb);
            _dbContext.SaveChanges();
        }

        //Tar ut allt som man säljer för en användare
        public List<Auction> GetAllBySeller(string seller)
        {
            // eager loading
            var auctionDbs = _dbContext.AuctionDbs
                .Where(p => p.Seller.Equals(seller)) 
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDbs)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                result.Add(auction);
            }

            return result;
        }

        //Tar ut alla auctions som man lagt bud på
        public List<Auction> GetAllByBidder(string bidder)
        {
            // eager loading
            var auctionDbs = _dbContext.AuctionDbs
                .Include(a => a.BidDbs)
                .ToList();

            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDbs)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                if (auctionDbs != null)
                {
                    foreach (BidDb pdb in adb.BidDbs)
                    {
                        auction.AddBid(_mapper.Map<Bid>(pdb));
                    }
                }
                foreach (Bid bid in auction._bids)
                {
                    if (bid.Bidder.Equals(bidder) && !result.Contains(auction) && !auction.IsExpired())
                    {
                        result.Add(auction);
                    }
                }
            }
            return result;
        }

        //Tar ut alla auctions
        public List<Auction> GetAllAuctions()
        {
            var auctionDb = _dbContext.AuctionDbs
                .Include(p => p.BidDbs) 
                .ToList();
            List<Auction> result = new List<Auction>();
            foreach (AuctionDb adb in auctionDb)
            {
                Auction auction = _mapper.Map<Auction>(adb);
                if (!auction.IsExpired())
                {
                    result.Add(auction);
                }
            }
            return result;
        }

        public void AddBid(Auction auction, Bid bid)
        {
           AuctionDb ad = _mapper.Map<AuctionDb>(auction);
            _dbContext.ChangeTracker.Clear();
           BidDb bdb = _mapper.Map<BidDb>(bid);
            bdb.AuctionDb = ad;
            _dbContext.BidDbs.Add(bdb);
            _dbContext.Entry(ad).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void UpdateDescription(Auction auction)
        {
            AuctionDb ad = _mapper.Map<AuctionDb>(auction);
            _dbContext.ChangeTracker.Clear();
            _dbContext.AuctionDbs.Update(ad);
            _dbContext.Entry(ad).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

    }
}