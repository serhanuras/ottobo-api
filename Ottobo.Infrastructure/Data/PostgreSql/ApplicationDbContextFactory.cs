using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ottobo.Infrastructure.Data.PostgreSql
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=ottobo.c0d8nzigxluc.eu-west-2.rds.amazonaws.com;Database=ottobodb;Username=postgres;Password=SDFwer741");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}