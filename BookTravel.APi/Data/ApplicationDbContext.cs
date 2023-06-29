using BookTravel.APi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookTravel.APi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.HasSequence("AppointmentSeq", schema: "Ticket").StartsAt(1_000_000).IncrementsBy(1);
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRole", schema: "Auth");
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers", schema: "Auth");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaim", schema: "Auth");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogin", schema: "Auth");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserToken", schema: "Auth");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaim", schema: "Auth");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRole", schema: "Auth");

            modelBuilder.Entity<Appointment>().ToTable("Appointments", schema: "Ticket")
                .Property(p => p.Id).HasDefaultValueSql("NEXT VALUE FOR Ticket.AppointmentSeq");

            modelBuilder.Entity<Bus>().ToTable("Buses", schema: "Ticket");
            modelBuilder.Entity<Booking>().ToTable("Bookings", schema: "Ticket");
            
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
