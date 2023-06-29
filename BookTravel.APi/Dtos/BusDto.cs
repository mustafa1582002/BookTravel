using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace BookTravel.APi.Dtos
{
    public class BusDto
    {
        [AssertThat("Capacity >= 8")]
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
