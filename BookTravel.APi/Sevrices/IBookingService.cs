using BookTravel.APi.Models;

namespace BookTravel.APi.Sevrices
{
    public interface IBookingService : IBaseService<Booking>
    {
        Task<Booking?> GetBookingAsync(string UserId, long AppointmentId);
        void ConfiremBooking(Booking Booking);
    }
}
