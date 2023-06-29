using BookTravel.APi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTravel.APi.Sevrices
{
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        public AppointmentService(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Appointment>> GetAvailableAppointmentAsync()
        {
            return await _context.Appointments.Where(p => p.NumberTravelers != 0).Include(p => p.Buses).ToListAsync();
        }
    }
}
