using Assessment_dtic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Assessment_dtic.Models;

namespace Assessment_dtic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
