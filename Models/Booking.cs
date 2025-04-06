using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        // Venue ID - Foreign Key to Venue table
        [Required(ErrorMessage = "Venue is required.")]
        public int VenueId { get; set; }

        // Event ID - Foreign Key to Event table
        [Required(ErrorMessage = "Event is required.")]
        public int EventId { get; set; }

        // Booking Date - Required, needs to be a future date
        [Required(ErrorMessage = "Booking date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Booking Date")]
        [FutureDate(ErrorMessage = "Booking date must be in the future.")]
        public DateTime BookingDate { get; set; }

        // Name of the person making the booking
        [Required(ErrorMessage = "Booker's name is required.")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string BookerName { get; set; }

        // Email of the person making the booking
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string BookerEmail { get; set; }

        // Navigation properties
        public Venue Venue { get; set; } // Navigation to Venue model
        public Event Event { get; set; } // Navigation to Event model
    }

    // Custom validation attribute for future booking date
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateValue = (DateTime)value;
            return dateValue > DateTime.Now;
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