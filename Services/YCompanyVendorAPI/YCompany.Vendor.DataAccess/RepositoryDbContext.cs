using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using YCompany.Vendor.Domain.Entities;

namespace YCompany.Vendor.DataAccess
{
    public sealed class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Vendors> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
    }
}
