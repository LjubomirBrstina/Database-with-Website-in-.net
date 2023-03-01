using ProjectApp.Core;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Persistence
{
    public class AuctionDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string AuctionName { get; set; }

        [Required]
        public int StartingPrice { get; set; }

        [Required]
        [MaxLength(128)]
        public string Description { get; set; }

        [Required]
        public string Seller { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpireDate { get; set; }

        public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
    }
}
