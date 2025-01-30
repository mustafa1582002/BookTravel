using BookTravel.APi.Dtos;
using BookTravel.APi.Models;

namespace BookTravel.APi.Sevrices
{
    public interface ITravelerService
    {
        
        Task<ApplicationUser?> IsTravelerAsync(string UserId);
        Task<string?> CreateTravelerAsync(ApplicationUser Traveler, string Password);
        Task<string?> UpdateTravelerAsync(ApplicationUser Traveler);
        Task<IEnumerable<TravelerViewDto>> RetrieveTravelersAsync();
        Task<string?> DeleteTravelerAsync(ApplicationUser User);
    }
}
