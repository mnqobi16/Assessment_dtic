using System.ComponentModel.DataAnnotations;

namespace Assessment_dtic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string AppointmentType { get; set; } // Annual Medical, Dental, etc.
        public DateTime AppointmentDateTime{ get; set; }
        [Required]
        public string AppointmentTimeSlot { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

    }
}
