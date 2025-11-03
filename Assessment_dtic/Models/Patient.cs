namespace Assessment_dtic.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string IDNumber { get; set; } = string.Empty; // 13-digit SA ID
        public string Gender { get; set; } = string.Empty;
        public int NumberOfChildren { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Suburb { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
