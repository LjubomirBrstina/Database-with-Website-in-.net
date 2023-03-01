using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.Persistence
{
    public class BidDb
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Bidder { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimeSubmitted { get; set; }

        // FK and navigation property
        [ForeignKey("AuctionId")]
        public AuctionDb? AuctionDb { get; set; }
        
        public int AuctionId { get; set; }
    }
}
