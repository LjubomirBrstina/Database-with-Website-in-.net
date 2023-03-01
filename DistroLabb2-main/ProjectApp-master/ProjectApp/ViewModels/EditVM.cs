using ProjectApp.Core;
using System.ComponentModel.DataAnnotations;

namespace ProjectApp.ViewModels
{
    public class EditVM
    {
        [Required]
        [StringLength(128, ErrorMessage = "Max length 128 characters")]
        public string Description { get; set; }
    }
}
