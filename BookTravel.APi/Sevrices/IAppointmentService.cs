using BookTravel.APi.Models;

namespace BookTravel.APi.Sevrices
{
    public interface IAppointmentService : IBaseService<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAvailableAppointmentAsync();
    }
}
