namespace EventEaseApp.Models
{
    public class EventRegistration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int EventId { get; set; } // Added EventId property
    }
}