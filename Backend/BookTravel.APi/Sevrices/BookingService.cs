using BookTravel.APi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTravel.APi.Sevrices
{
    public class BookingService : BaseService<Booking>, IBookingService
    {
        public BookingService(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Booking> CreateAsync(Booking Booking)
        {
            await _context.Bookings.AddAsync(Booking);
            _context.SaveChanges();
            return Booking;
        }
        public void ConfiremBooking(Booking Booking)
        {
            Booking.IsConfirmed = true;
            _context.SaveChanges();
        }
        public async Task<Booking?> GetBookingAsync(string UserId ,long AppointmentId)
        {
            return await _context.Bookings.Where(p => p.UserId == UserId && p.AppointmentId == AppointmentId && !p.IsConfirmed).FirstOrDefaultAsync();

        }

    }
}
