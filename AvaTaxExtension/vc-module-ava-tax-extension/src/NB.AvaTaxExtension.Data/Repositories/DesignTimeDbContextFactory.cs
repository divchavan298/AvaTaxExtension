using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NB.AvaTaxExtension.Data.Repositories
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AvaTaxExtensionDbContext>
    {
        public AvaTaxExtensionDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AvaTaxExtensionDbContext>();

            builder.UseSqlServer("Data Source=(local);Initial Catalog=VirtoCommerce3;Persist Security Info=True;User ID=virto;Password=virto;MultipleActiveResultSets=True;Connect Timeout=30");

            return new AvaTaxExtensionDbContext(builder.Options);
        }
    }
}