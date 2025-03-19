using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<User> Users { get; set; } // Getting/setting the Users column in the database
        public DbSet<Event> Events { get; set; } // Getting/setting the Events column in the database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);  

            modelBuilder.Entity<Event>() // creating a foreign-key relationship in teh database between Users and Events;  all events must be linked with a User's unique ID, and in the event the user is deleted their events will be as well.  
                .HasOne(e => e.user)
                .WithMany(u => u.Events)
                .HasForeignKey (e => e.userId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
};