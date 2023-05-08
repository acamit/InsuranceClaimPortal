using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YCompany.Payments.Domain.Enitites;

namespace YCompany.Payments.DataAccess
{
    public sealed class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
    }
}
