using BookTravel.APi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace BookTravel.APi.Dtos
{
    public class AppointmentDto
    {
        [MaxLength(100)]
        public string From { get; set; } = null!;
        [MaxLength(100)]
        public string To { get; set; } = null!;
        [AssertThat("StartTime >= Today()")]
        public DateTime StartTime { get; set; }
        [AssertThat("EndTime >= Today() && EndTime > StartTime")]
        public DateTime EndTime { get; set; }
        public float TicketPrice { get; set; }
        public IList<int> Buses { get; set; } = null!;
    }
}
