using Microsoft.EntityFrameworkCore;
using YCompany.Claims.Domain.Entities;

namespace YCompany.Claims.DataAccess
{
    public sealed class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Claim> ClientClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
    }
}
