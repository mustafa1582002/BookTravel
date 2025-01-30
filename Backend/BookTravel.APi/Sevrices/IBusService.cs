using BookTravel.APi.Dtos;
using BookTravel.APi.Models;

namespace BookTravel.APi.Sevrices
{
    public interface IBusService :IBaseService<Bus>
    {
        void EnableBuses(List<Bus> buses);
        Task<IEnumerable<Bus>> GetAvailableBusesAsync();
    }
}
