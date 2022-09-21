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
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter your User Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "User Name")]
        [Key]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }
        public Boolean Deleted { get; set; }
        [Display(Name = "Created on")]
        [DisplayFormat(DataFormatString = "{HH:mm:ss, dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Last Edit on")]
        [DisplayFormat(DataFormatString = "{HH:mm:ss, dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastEdit { get; set; }
        public User()
        {
            Deleted = false;
            CreatedDate = DateTime.Now;
            LastEdit = DateTime.Now;
        }
    }

}