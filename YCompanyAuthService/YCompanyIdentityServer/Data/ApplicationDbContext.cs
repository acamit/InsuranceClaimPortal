using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YCompanyIdentityServer.Models;

namespace YCompanyIdentityServer.Data
{
    /// <summary>
    /// DB Context for asp.net core identity is injected here. 
    /// we tell the asp.net 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // customer user class that extends identity user class.
    {
        // we are going to have multiple db contexts. One from identity server and other for microsoft.identity. By specifying type, Dependency injection wlll know which database context to pass when passing options
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
