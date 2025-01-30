using BookTravel.APi.Models;
using Microsoft.AspNetCore.Identity;

namespace BookTravel.APi.Conts
{
    public class SeedAdmin
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            if (!_userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = "Admin@Booking.com",
                    UserName="Adimn"
                };
                await _userManager.CreateAsync(user, "P@ssWord123");
                await _userManager.AddToRoleAsync(user, AppRole.Admin);
            }
        }
    }
}
