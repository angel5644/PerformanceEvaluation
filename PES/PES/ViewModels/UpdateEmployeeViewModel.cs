using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PES.ViewModels
{
    public class UpdateEmployeeViewModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Year evaluation is required")]
        [Display(Name = "Evaluation Year")]
        public int EvaluationYear { get; set; }

        public List<SelectListItem> ListPeriods { get; set; }
        [Required(ErrorMessage = "Period is required")]
        [Display(Name = "Period")]
        public int SelectedPeriod { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        //[DataType(DataType.EmailAddress)]
        //[EmailAddress(ErrorMessage = "The email address doesn't have the correct format")]
        public string Email { get; set; }

        public List<SelectListItem> ListProfiles { get; set; }
        [Display(Name = "Profile")]
        public int SelectedProfile { get; set; }

        public List<SelectListItem> ListManagers { get; set; }
        [Display(Name = "Manager")]
        public int SelectedManager { get; set; }

        public string TextEmail
        {
            get
            {
                return "@4thsource.com";
            }
            set {   }
        }
        public List<SelectListItem> ListLocation { get; set; }
        [Display(Name = "Location")]
        public int SelectedLocation { get; set; }

        //public string Customer { get; set; }
        //public string Position { get; set; }
        //public string Project { get; set; }
    }
}