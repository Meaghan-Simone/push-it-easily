using Microsoft.EntityFrameworkCore;
using EventEase.Models;

namespace EventEase.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Booking>() //https://openai.com/index/chatgpt/ for error handling as there was a cascading error 
                .HasOne(b => b.Event)
                .WithMany() // (Tdykstra, 2024)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Cascade);

          
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Venue)
                .WithMany() // (Tdykstra, 2024)
                .HasForeignKey(b => b.VenueId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
//Bibliography 
//Rick-Anderson. (2024, September 27). Tag Helpers in forms in ASP.NET Core. Microsoft Learn. https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-7.0#working-with-dropdown-lists
////SamMonoRT. (2024, November 12). Overview of Entity Framework Core - EF Core. Microsoft Learn. https://learn.microsoft.com/en-us/ef/core/ 
//Tdykstra. (2024a, April 10). Tutorial: Implement CRUD Functionality - ASP.NET MVC with EF Core. Microsoft Learn. https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/crud?view=aspnetcore-7.0
//Tdykstra. (2024b, September 18). Dependency injection in ASP.NET Core. Microsoft Learn. https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-9.0
//W3Schools.com. (n.d.-a). https://www.w3schools.com/css/css_image_transparency.asp W3Schools.com. (n.d.-b). https://www.w3schools.com/cssref/pr_background-image.php
//W3Schools.com. (n.d.-c). https://www.w3schools.com/css/css_form.asp
//https://openai.com/index/chatgpt/