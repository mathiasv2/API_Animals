using System;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkshop.Models
{
	public class BookingAnimalsDbContext : DbContext 
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Animals> Animals { get; set; }
		public DbSet<Species> Species { get; set; }

		public BookingAnimalsDbContext(DbContextOptions options) : base(options)
		{
		}

        public BookingAnimalsDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //user
            modelBuilder.Entity<User>()
                .HasKey(x => x.id);

            modelBuilder.Entity<User>()
                .HasData(
                new User { id = 1, name = "admin", email = "admin@gmail.com", password = "Administrator1!" }
                );

            // species 
            modelBuilder.Entity<Species>()
                .HasKey(x => x.id);

            modelBuilder.Entity<Species>()
                .HasMany(x => x.Animals)
                .WithOne(x => x.Species)
                .HasForeignKey(x => x.SpeciesId);

            modelBuilder.Entity<Species>()
                .HasData(
                    new Species { id = 1, name = "chien" },
                    new Species { id = 2, name = "chat" }
                );

            // animals 
            modelBuilder.Entity<Animals>()
                .HasKey(x => x.id);

            modelBuilder.Entity<Animals>()
                .HasData(
                    new Animals { id = 1, name = "rex", SpeciesId = 1 },
                    new Animals { id = 2, name = "mistrigri", SpeciesId = 2 }
                );

            base.OnModelCreating(modelBuilder);
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			var connectionString = "server=localhost;port=3306;database=bookingAnimals;user=root;password=root;";
			optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}
    }
}

