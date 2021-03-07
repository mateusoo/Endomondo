using System;
using System.IO;
using Endomondo.Models;
using Microsoft.EntityFrameworkCore;
using Location = Endomondo.Models.Location;

namespace Endomondo.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }

        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal), 
                "EndomondoV2.db3");

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
