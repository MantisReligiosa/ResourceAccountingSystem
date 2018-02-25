using DomainInterfaces;

namespace DomainObjects
{
    public class Meter : IIdentified
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public int ReadingValue { get; set; }
        public House House { get; set; }
    }
}
