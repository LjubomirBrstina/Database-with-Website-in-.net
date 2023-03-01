using ProjectApp.Core;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ProjectApp.ViewModels
{
    public class CreateAuctionVM
    {

        [Required]
        [StringLength(128, ErrorMessage = "Max length 128 characters")]
        public string AuctionName { get; set; }

        [StringLength(128, ErrorMessage = "Max length 128 characters")]
        public string? Seller { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Max length 128 characters")]
        public string Description { get; set; }

        [Required]
        [Range(1,10000000, ErrorMessage = "Enter a positive number")]
        public int StartingPrice { get; set; }
        
        [Required]
        public DateTime ExpireDate { get; set; }
    }
}
