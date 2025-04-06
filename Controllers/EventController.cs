using EventEase.Data;
using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace EventEase.Controllers
{
    public class EventController : Controller
    {
        //Incorporate the database  (SamMonoRT, 2024)
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var eventList = await _context.Events
                .Include(e => e.Venue)
                .ToListAsync();

            return View(eventList);
            //Pass the events list to the view (SamMonoRT, 2024)
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "Id", "Name");
            return View(new Event());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event eventItem)//Handles form submission and saves the new event to the database (Rick-Anderson, 2024)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Add(eventItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "Id", "Name", eventItem.VenueId);
            return View(eventItem);
        }


        public async Task<IActionResult> Edit(int id) //Edit
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null) return NotFound();

            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "Id", "Name", eventItem.VenueId);
            return View(eventItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Event eventItem)
        {
            if (ModelState.IsValid)
            {
                _context.Events.Update(eventItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = new SelectList(await _context.Venues.ToListAsync(), "Id", "Name", eventItem.VenueId);
            return View(eventItem);
        }

        public async Task<IActionResult> Delete(int id) //Delete (SamMonoRT, 2024)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null) return NotFound();

            return View(eventItem);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);
            if (eventItem == null) return NotFound();

            _context.Events.Remove(eventItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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