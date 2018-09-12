using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using PES.Models;

namespace PES.ViewModels
{
    public class ChangeProfileViewModel : InsertEmployeeViewModel
    {
        [Display(Name = "Transfer all to ")]
        public int NewManager { get; set; }
        public int Assigned { get; set; }
        [Display(Name = "Current Profile")]
        public Profile CurrentProfile { get; set; } = new Profile();
        public List<UpdateEmployeeViewModel> Org { get; set; } = new List<UpdateEmployeeViewModel>();
    }
}