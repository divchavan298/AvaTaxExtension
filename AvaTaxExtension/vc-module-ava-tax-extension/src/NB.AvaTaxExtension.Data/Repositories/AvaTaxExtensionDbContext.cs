using EntityFrameworkCore.Triggers;
using Microsoft.EntityFrameworkCore;

namespace NB.AvaTaxExtension.Data.Repositories
{
    public class AvaTaxExtensionDbContext : DbContextWithTriggers
    {
        public AvaTaxExtensionDbContext(DbContextOptions<AvaTaxExtensionDbContext> options)
            : base(options)
        {
        }

        protected AvaTaxExtensionDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<AvaTaxExtensionEntity>().ToTable("AvaTaxExtension").HasKey(x => x.Id);
            //modelBuilder.Entity<AvaTaxExtensionEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();
        }
    }
}