using DomainObjects;
using System.Data.Entity.ModelConfiguration;

namespace DatabaseRepository.Mapping
{
    public class HouseConfiguration : EntityTypeConfiguration<House>
    {
        public HouseConfiguration()
        {
            ToTable("House");
            HasKey(h => h.Id);
            Property(h => h.Id).HasColumnName("Id").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(h => h.Address).HasColumnName("Address");
            HasOptional(h => h.Meter).WithRequired(m => m.House);
        }
    }
}