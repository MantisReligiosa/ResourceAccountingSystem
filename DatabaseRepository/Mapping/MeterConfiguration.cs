using DomainObjects;
using System.Data.Entity.ModelConfiguration;

namespace DatabaseRepository.Mapping
{
    public class MeterConfiguration : EntityTypeConfiguration<Meter>
    {
        public MeterConfiguration()
        {
            ToTable("Meter");
            HasKey(m => m.Id);
            Property(m => m.Id);
            Property(m => m.ReadingValue).HasColumnName("ReadingValue");
            Property(m => m.Serial).HasColumnName("Serial");
            Ignore(m => m.House);
        }
    }
}