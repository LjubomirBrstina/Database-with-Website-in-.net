using AutoMapper;
using ProjectApp.Core;
using ProjectApp.Persistence;
using Bid = ProjectApp.Core.Bid;

namespace ProjectApp.Mappings
{
    public class BidProfile : Profile
    {
        public BidProfile()
        {
            // Default mapping when property names are same
            CreateMap<BidDb, Bid>()
                   .ReverseMap();
        }
    }
}
