using AutoMapper;
using BookTravel.APi.Dtos;
using BookTravel.APi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTravel.APi.Sevrices
{
    public class BusService : BaseService<Bus>, IBusService
    {
        public BusService(ApplicationDbContext context):base(context)
        {

        }   

        public void EnableBuses(List<Bus> buses)
        {
            foreach(var bus in buses)
            {
                bus.IsAvailable = true;
                bus.AppointmentId = null;

                _context.Update(bus);
               
            }
            _context.SaveChanges();
        }
        public async Task<IEnumerable<Bus>> GetAvailableBusesAsync()
        {
            return await _context.Buses.Where(b=>b.IsAvailable).OrderBy(b=>b.Id).ToListAsync();
        }
    }
}
