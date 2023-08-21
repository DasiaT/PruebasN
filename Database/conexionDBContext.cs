using Microsoft.EntityFrameworkCore;
using NinjaTalentCountrys.Models;

namespace Delivery_Backend.Models
{
    public class InterfacesDBContext : DbContext
    {
        public InterfacesDBContext(DbContextOptions<InterfacesDBContext> options) : base(options) { }
        public DbSet<CountryModel> Country { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<CountryModel>().ToTable("Country").HasNoKey();


        }
    }
}
