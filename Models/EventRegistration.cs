using System.ComponentModel.DataAnnotations;

namespace EventEaseApp.Models
{
    public class EventRegistration
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [Required]
        public int EventId { get; set; } // Added EventId property

        // Navigation property to the related event
        public Event Event { get; set; }
    }
}