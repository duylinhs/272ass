using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _272ass.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your User Name")]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }
        [NotMapped]
        public List<SelectListItem> Roles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "admin", Text = "Administrator" },
            new SelectListItem { Value = "organiser", Text = "Event Organiser" },
            new SelectListItem { Value = "attendee", Text = "User"  },
        };
        public Boolean Deleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastEdit { get; set; }
    }
}