using ProjectApp.Core;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.ViewModels
{
    public class CreateBidVM
    {
        [Required]
        [Range(1, 10000000, ErrorMessage = "Enter a positive number")]
        public int Amount { get; set; }
    }
}
