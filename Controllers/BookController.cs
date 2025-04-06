using Microsoft.AspNetCore.Mvc;
using EventEase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using EventEase.Data;

namespace EventEase.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Implementing ApplicationDbContext into the controller (SamMonoRT, 2024)
        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Booking Index
        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Bookings.Include(b => b.Venue).Include(b => b.Event).ToListAsync();
            return View(bookings);
        }

        // Booking Create 
        public IActionResult Create()
        {
            // Populate the ViewBag with events and venues data for the dropdowns (Rick-Anderson, 2024)
            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName");
            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "Name");

            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,EventId,VenueId,BookingDate,BookerName,BookerEmail")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // (Saves) the new booking (SamMonoRT, 2024)
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Return to create if there is a validation error
            ViewBag.Events = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            ViewBag.Venues = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        //Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Populates 
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,EventId,VenueId,BookingDate,CustomerName,CustomerEmail")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            //Populates if there is an error (Rick-Anderson, 2024)
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        //Delete Booking (SamMonoRT, 2024)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // This will check if the booking is already in the database
        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
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