using System.ComponentModel.DataAnnotations;

namespace FUNewsManagement.Models
{
    public class AccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
