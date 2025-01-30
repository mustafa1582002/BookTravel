namespace BookTravel.APi.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public long? AppointmentId { get; set; }    
    }
}
