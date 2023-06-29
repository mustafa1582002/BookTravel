using BookTravel.APi.Dtos;
using BookTravel.APi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace BookTravel.APi.Sevrices
{
    public interface IAuthService
    {
        Task<AuthModelDto> LoginAsync(ApplicationUser User);
        Task LogoutAsync(ApplicationUser User);
        Task<ApplicationUser?> GetUserAsync(string Key);
        Task<ApplicationUser?> IsRegisterdAsync(string Email, string Password);
        Task<ApplicationUser> UpdatePassWordAsync(ApplicationUser User, string NewPassword);
    
    }
}
