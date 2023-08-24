﻿using Microsoft.EntityFrameworkCore;
using NinjaTalentCountrys.Models;

namespace Delivery_Backend.Models
{
    public class InterfacesDBContext : DbContext
    {
        public InterfacesDBContext(DbContextOptions<InterfacesDBContext> options) : base(options) { }
        public DbSet<CountryModel> CountryModel { get; set; }

        //public DbSet<RestCountryModel> RestCountryModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<CountryModel>().ToTable("country");
        }
    }
}
