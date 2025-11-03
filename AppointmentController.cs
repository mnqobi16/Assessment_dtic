using Assessment_dtic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Assessment_dtic.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Make PatientOptions nullable to fix the warning
        public SelectList? PatientOptions { get; set; }

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Change Index to return IActionResult and initialize PatientOptions
        public IActionResult Index()
        {
            PatientOptions = new SelectList(_context.Patients, "Id", "Name");
            return View();
        }

        // Fix OnPostAsync to redirect to Index action
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(Models.Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate PatientOptions if model state is invalid
                PatientOptions = new SelectList(_context.Patients, "Id", "Name");
                return View("Index");
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
