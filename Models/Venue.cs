using System.ComponentModel.DataAnnotations;
using EventEase.Models;


namespace EventEase.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(255)]
        public required string Location { get; set; }

        [Required]
        public int Capacity { get; set; }

        [StringLength(1000)] // Optional: limit length
        public string? Description { get; set; }


        public string? ImageUrl { get; set; } // URL to venue image (optional)

        // Navigation property for related Bookings
        
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
