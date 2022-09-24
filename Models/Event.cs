using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Diagnostics;

namespace _272ass.Models
{
    public class Event
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter the event name")]
        [Display(Name = "Title")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Created on")]
        public DateTime Created { get; set; }

        [Display(Name = "Edited on")]
        public DateTime LastEdit { get; set; }

        [Display(Name = "Seminar Time")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Registration Begins")]
        public DateTime RegistrationStart { get; set; }

        [Display(Name = "Registration Ends")]
        public DateTime RegistrationEnd { get; set; }
        public string Location { get; set; }

        public Boolean Deleted { get; set; }

        [ForeignKey("Organiser")]
        public int OrganiserID { get; set; }
        [Display(Name = "Creator")]
        [Required]
        public virtual Organiser Organiser { get; set; }

        [ForeignKey("EventType")]
        public int EventTypeID { get; set; }

        [Display(Name = "Seminar Type")]
        public virtual EventType EventType { get; set; }

        public decimal Price { get; set; }
        public virtual ICollection<Attendee> Attendees { get; set; }
        public Event()
        {
            Deleted = false;
            Created = DateTime.Now;
            Attendees = new List<Attendee>();
            LastEdit = DateTime.Now;
        }
    }
}