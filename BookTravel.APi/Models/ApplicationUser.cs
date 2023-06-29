using Microsoft.AspNetCore.Identity;

namespace BookTravel.APi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }
        [MaxLength(100)]
        public string? LastName { get; set; }
        public bool Status { get; set; }

    }
}
