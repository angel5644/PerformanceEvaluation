using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PES.ViewModels
{
    public class InsertEmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid")]
        //[EmailAddress(ErrorMessage = "The email address doesn't have the correct format.")]
        //[RegularExpression("(.*?)@4thsource.com" , ErrorMessage = "This email is not a 4th Source account" )]
        public string Email { get; set; }

        public List<SelectListItem> ListProfiles { get; set; }
        [Display(Name = "Profile")]
        public int SelectedProfile { get; set; }

        public List<SelectListItem> ListManagers { get; set; }
        [Display(Name = "Manager")]
        public int SelectedManager { get; set; }

        //[Display(Name = "Hire Date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/mm/y}", ApplyFormatInEditMode = true)]
        //public DateTime HireDate { get; set; }     

        public string TextEmail
        {
            get
            {
                return  "@4thsource.com";
            }
            set { }
        }

        public List<SelectListItem> ListLocations { get; set; }
        [Display(Name = "Location")]
        public int SelectedLocation { get; set; }
    }
}
