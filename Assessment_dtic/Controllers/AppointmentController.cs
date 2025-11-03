using Assessment_dtic.Data;
using Assessment_dtic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assessment_dtic.Controllers
{
    public class AppointmentController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}


        private readonly ApplicationDbContext _context;
        public SelectList PatientOptions { get; set; } // Add this line

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = _context.Appointments.Include(a => a.Patient).ToList();
            return View(appointments);
        }
        public IActionResult Create()
        {
            ViewBag.TimeSlotOptions = GetTimeSlots();
            ViewBag.TypeOptions = GetAppointmentTypes();
            ViewBag.PatientOptions = new SelectList(_context.Patients, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            // Prevent past dates
            if (appointment.AppointmentDateTime < DateTime.Now.Date)
            {
                ModelState.AddModelError("Date", "Please select a future date.");
            }

            // Check if slot is already booked
            bool isTaken = _context.Appointments
                .Any(a => a.AppointmentDateTime == appointment.AppointmentDateTime && a.AppointmentTimeSlot == appointment.AppointmentTimeSlot);

            if (isTaken)
            {
                ModelState.AddModelError("TimeSlot", "This time slot is already booked for the selected date.");
            }

            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TimeSlotOptions = GetTimeSlots();
            ViewBag.TypeOptions = GetAppointmentTypes();
            ViewBag.PatientOptions = new SelectList(_context.Patients, "Id", "Name", appointment.PatientId);
            return View(appointment);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();
            ViewBag.TimeSlotOptions = GetTimeSlots();
            ViewBag.TypeOptions = GetAppointmentTypes();
            ViewBag.PatientOptions = new SelectList(_context.Patients, "Id", "Name", appointment.PatientId);
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Appointment appointment)
        {
            if (id != appointment.Id) return NotFound();

            // Prevent past dates
            if (appointment.AppointmentDateTime < DateTime.Now.Date)
            {
                ModelState.AddModelError("Date", "Please select a future date.");
            }

            // Check if slot is already booked
            bool isTaken = _context.Appointments
                .Any(a => a.AppointmentDateTime == appointment.AppointmentDateTime && a.AppointmentTimeSlot == appointment.AppointmentTimeSlot);

            if (isTaken)
            {
                ModelState.AddModelError("TimeSlot", "This time slot is already booked for the selected date.");
            }


            if (ModelState.IsValid)
            {
                _context.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TimeSlotOptions = GetTimeSlots();
            ViewBag.TypeOptions = GetAppointmentTypes();
            ViewBag.PatientOptions = new SelectList(_context.Patients, "Id", "Name", appointment.PatientId);
            return View(appointment);
        }

        // GET: Appointment/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var appointment = _context.Appointments
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.Id == id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var appointment = _context.Appointments
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.Id == id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        
        private List<SelectListItem> GetAppointmentTypes()
        {
            return new List<SelectListItem>
    {
        new SelectListItem { Value = "First Consultation", Text = "First Consultation" },
        new SelectListItem { Value = "Follow-up", Text = "Follow-up" },
        new SelectListItem { Value = "Emergency", Text = "Emergency" },
        new SelectListItem { Value = "Chronic Management", Text = "Chronic Management" },
        new SelectListItem { Value = "Injury on duty", Text = "Injury on duty" }
    };
        }

        private List<SelectListItem> GetTimeSlots()
        {
            var slots = new List<SelectListItem>();
            var start = new TimeSpan(9, 0, 0);
            var end = new TimeSpan(16, 0, 0);

            while (start < end)
            {
                var next = start.Add(TimeSpan.FromMinutes(15));
                var slotText = $"{start:hh\\:mm}–{next:hh\\:mm}";
                slots.Add(new SelectListItem { Value = slotText, Text = slotText });
                start = next;
            }

            return slots;
        }


    }
}
