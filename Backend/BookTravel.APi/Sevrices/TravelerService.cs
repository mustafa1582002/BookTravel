using AutoMapper;
using BookTravel.APi.Data;
using BookTravel.APi.Dtos;
using BookTravel.APi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookTravel.APi.Sevrices
{
    public class TravelerService : ITravelerService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TravelerService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
        }
        public async Task<string?> CreateTravelerAsync(ApplicationUser Traveler, string Password)
        {
            var Result = await _userManager.CreateAsync(Traveler, Password);
            if (!Result.Succeeded)
                return string.Join(',', Result.Errors.Select(d => d.Description).ToArray());

            var ResultRole = await _userManager.AddToRoleAsync(Traveler, AppRole.Traveler);
            if (!ResultRole.Succeeded)
                return string.Join(',', ResultRole.Errors.Select(d => d.Description).ToArray());

            return string.Empty;
        }
        public async Task<string?> UpdateTravelerAsync(ApplicationUser Traveler)
        {
            var Result = await _userManager.UpdateAsync(Traveler);
            if (!Result.Succeeded)
                return string.Join(',', Result.Errors.Select(d => d.Description).ToArray());

            return string.Empty;
        }

        public async Task<IEnumerable<TravelerViewDto>> RetrieveTravelersAsync()
        {
            var TravelRoleId = await _roleManager.Roles.Where(p => p.Name == AppRole.Traveler)
                .Select(p => p.Id).SingleOrDefaultAsync();

            var Travelers = (from Traveler in _context.Users
                             join Roles in _context.UserRoles
                             on Traveler.Id equals Roles.UserId
                             select new
                             {
                                 Roles.RoleId,
                                 Traveler.Id,
                                 Traveler.FirstName,
                                 Traveler.LastName,
                                 Traveler.Email,
                                 Traveler.Status
                             })
                             .Where(o => o.RoleId == TravelRoleId).Select(t => new TravelerViewDto
                             {
                                 Id = t.Id,
                                 Email = t.Email,
                                 FirstName = t.FirstName,
                                 LastName = t.LastName,
                                 Status = t.Status
                             }).ToList();

            return Travelers;
        }

        public async Task<string?> DeleteTravelerAsync(ApplicationUser User)
        {
            var ResultRole=await _userManager.RemoveFromRoleAsync(User, AppRole.Traveler);
            if (!ResultRole.Succeeded)
                return string.Join(',', ResultRole.Errors.Select(p => p.Description).ToArray());

            var result=await _userManager.DeleteAsync(User);
            if (!result.Succeeded)
                return string.Join(',', result.Errors.Select(p => p.Description).ToArray());

            return string.Empty;
        }

        public async Task<ApplicationUser?> IsTravelerAsync(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null || !await _userManager.IsInRoleAsync(user, AppRole.Traveler))
                return null;

            return user;
        }
    }
}
