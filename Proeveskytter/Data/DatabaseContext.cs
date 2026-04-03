using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proeveskytter.Models;

namespace Proeveskytter.Data
{
    public class DatabaseContext : IdentityDbContext
    {
        public DbSet<Indstilling> Indstillinger { get; set; }
        public DbSet<Skytte> Skytter { get; set; }
        public DbSet<Skydning> Skydninger { get; set; }

        public string DatabaseFilnavn { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}
