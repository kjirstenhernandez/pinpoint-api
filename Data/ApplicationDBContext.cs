using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.user)
                .WithMany(u => u.Events)
                .HasForeignKey (e => e.userId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
};

        //    var user1 = new User
        //     {
        //         id = "8800981e-d506-4462-9b76-4e653ade43b5",
        //         firstName = "Alice",
        //         lastName = "Johnson",
        //         locLat = 40.7128,
        //         locLon = -74.0060,
        //         email = "alice@example.com",
        //         phone = "123-456-7890"
        //     };

        //     var user2 = new User
        //     {
        //         id = "21ff28d2-dfb8-4397-a6d3-513363324cac",
        //         firstName = "Bob",
        //         lastName = "Smith",
        //         locLat = 34.0522,
        //         locLon = -118.2437,
        //         email = "bob@example.com",
        //         phone = "987-654-3210"
        //     };

        //     var event1 = new Event
        //     {
        //         id = "54dc5366-933f-4e39-87ac-6ec216014ce1",
        //         title = "Tech Conference",
        //         description = "A technology-focused convention.",
        //         lat = 37.7749,
        //         lon = -122.4194,
        //         type = 0,
        //         date = "Mar 30, 2025",
        //         time = "15:30:00",
        //         userId = user1.id
        //     };

        //     var event2 = new Event
        //     {
        //         id = Guid.NewGuid().ToString(),
        //         title = "Music Festival",
        //         description = "A fun outdoor festival.",
        //         lat = 36.1627,
        //         lon = -86.7816,
        //         type = EventType.festival,
        //         date = "Mar 30, 2025",
        //         time = "15:30:00",
        //         userId = user2.id
        //     };