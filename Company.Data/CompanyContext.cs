using Microsoft.EntityFrameworkCore;

namespace Company.Data.Context
{
    public class CompnayContext : DbContext
    {
        public CompnayContext(DbContextOptions<CompnayContext> options)
                : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            base.OnModelCreating(builder);
            builder.Entity<Company>();

        }

        public DbSet<Company> Company => Set<Company>();

        private void SeedData(ModelBuilder builder)
        {
            var companies = new List<Company>()
            {
                new Company() { Id = 1, CompanyName = "Company1"},
                new Company() { Id = 2, CompanyName = "Company2"},
            };
            builder.Entity<Company>().HasData(companies);

        }
    }
}