using System;
using System.ComponentModel.DataAnnotations;


namespace EventEaseApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Event Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Event Date is required")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; } = null;
        [Required(ErrorMessage = "Event Location is required")]
        public string Location { get; set; }
    }
}