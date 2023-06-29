using Microsoft.AspNetCore.Identity;

namespace BookTravel.APi.Conts
{
    public class SeedRole
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public SeedRole(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole(AppRole.Traveler));
                await _roleManager.CreateAsync(new IdentityRole(AppRole.Admin));
            }
        }
    }
}
