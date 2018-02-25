using DomainInterfaces;

namespace DomainObjects
{
    public class House : IIdentified
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public Meter Meter { get; set; }
    }
}
