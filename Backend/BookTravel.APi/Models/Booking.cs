using System.ComponentModel.DataAnnotations.Schema;

namespace BookTravel.APi.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser? User { get; set; }
        public long AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
