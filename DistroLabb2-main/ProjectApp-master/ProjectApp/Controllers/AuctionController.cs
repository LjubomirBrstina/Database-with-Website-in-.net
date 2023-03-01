using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectApp.Core;
using ProjectApp.Core.Interfaces;
using ProjectApp.ViewModels;

namespace ProjectApp.Controllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: ProjectsController
        public ActionResult Index()
        {
            string userName = User.Identity.Name; 
            List<Auction> auctions = _auctionService.GetAllAuctions();
            var result = auctions.OrderBy(x => x.ExpireDate);
            List<AuctionVM> auctionVMs = new();
            foreach(var auction in result)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        public ActionResult AuctionsBidOn()
        {
            string userName = User.Identity.Name; 
            List<Auction> auctions = _auctionService.GetAllByBidder(userName);
            var result = auctions.OrderBy(x => x.ExpireDate);
            List<AuctionVM> auctionVMs = new();

            foreach (var auction in result)
            {
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        // GET: ProjectsController/Details/5
        public ActionResult Details(String id)
        {
            Auction auction = _auctionService.GetByAuctionName(id);
            if (auction == null) return NotFound();

            Auction auction2 = auction;
            List<Bid> result = auction2._bids.ToList();
            result.Sort((a, b) => b.Amount.CompareTo(a.Amount));
            auction2._bids = result;
            AuctionDetailsVM auctionVM = AuctionDetailsVM.FromAuction(auction2);
            return View(auctionVM);
        }

        public ActionResult Edit(int id, string description, EditVM vm)
        {
            Auction auction = _auctionService.GetAuctionById(id);
            if (!auction.Seller.Equals(User.Identity.Name)) return InvalidTry();
            if (auction == null) return NotFound();
            if (ModelState.IsValid)
            {
                auction.Description = description;
                _auctionService.UpdateDescription(auction);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public ActionResult WonAuctions()
        {
            string userName = User.Identity.Name; // should be unique
            List<Auction> auctions = _auctionService.GetAuctionsWon(userName);
            var result = auctions.OrderBy(x => x.ExpireDate);
            List<AuctionVM> auctionVMs = new();
            foreach (var auction in result)
            {
                auction.StartingPrice = _auctionService.GetHighestBid(auction);
                auctionVMs.Add(AuctionVM.FromAuction(auction));
            }
            return View(auctionVMs);
        }

        // GET: ProjectsController/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Bid(int id)
        {
            Auction auction = _auctionService.GetAuctionById(id);
            if (auction == null) return NotFound();
            if (auction.Seller.Equals(User.Identity.Name)) return InvalidTry();
            return View();
        }

        public ActionResult InvalidTry()
        {
            return View("InvalidTry");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bid(int id, CreateBidVM vm)
        {
            Auction auction = _auctionService.GetAuctionById(id);
            if (auction == null) return NotFound();

            if (ModelState.IsValid)
            {
                int highestBid = _auctionService.GetHighestBid(auction);
                if (vm.Amount <= highestBid)
                {
                    ViewBag.Message = String.Format("Amount lower than current bid!");
                    return View();
                }
                Bid bid = new Bid(User.Identity.Name, vm.Amount, DateTime.Now);
                auction.AddBid(bid);
                _auctionService.AddBid(auction, bid);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // POST: ProjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuctionVM vm)
        {
            vm.Seller = User.Identity.Name;
            if (ModelState.IsValid)
            {
                if (vm.ExpireDate <= DateTime.Now)
                {
                    ViewBag.Message = String.Format("Past date can not be selected!");
                    return View();
                }
                Auction auction = new Auction()
                {
                    AuctionName = vm.AuctionName,
                    Description = vm.Description,
                    Seller = vm.Seller,
                    StartingPrice = vm.StartingPrice,
                    ExpireDate = vm.ExpireDate
                };
                _auctionService.AddAuction(auction);
                return RedirectToAction("Index");
            }
            return View(vm);
        }
    }
}
