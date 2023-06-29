using BookTravel.APi.Models;

namespace BookTravel.APi.Dtos
{
    public class BookingDto
    {
        public string UserId { get; set; } = null!;
        public long AppointmentId { get; set; }
    }
}
