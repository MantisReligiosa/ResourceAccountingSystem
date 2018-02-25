using DatabaseRepository.Mapping;
using DomainObjects;
using System.Data.Entity;
using System.Diagnostics;

namespace DatabaseRepository
{
    public class DatabaseContext : DbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Meter> Meters { get; set; }

        public DatabaseContext(string connectionString) :
            base(connectionString)
        {
#if DEBUG
            Database.Log = mess => Trace.Write(mess);
#endif
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new HouseConfiguration());
            modelBuilder.Configurations.Add(new MeterConfiguration());
        }
    }
}
