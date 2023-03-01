using AutoMapper;
using ProjectApp.Core;
using ProjectApp.Persistence;

namespace ProjectApp.Mappers
{
    public class AuctionProfile : Profile
    {
        public AuctionProfile()
        {
            // Default mapping when property names are same
            CreateMap<AuctionDb, Auction>()
               .ReverseMap();
        }
    }
}
