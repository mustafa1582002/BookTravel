namespace BookTravel.APi.Models
{
    public class Appointment
    {
        public long Id { get; set; }
        [MaxLength(100)]
        public string From { get; set; } = null!;
        [MaxLength(100)]
        public string To { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float TicketPrice { get; set; }
        public int NumberTravelers { get; set; }
        public ICollection<Bus> Buses { get; set; } = new List<Bus>();

    }
}
