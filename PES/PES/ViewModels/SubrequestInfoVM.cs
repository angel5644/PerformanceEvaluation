using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PES.ViewModels
{
    public class SubrequestInfoVM
    {
        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Return date
        /// </summary>
        public string ReturnDate { get; set; }

        /// <summary>
        /// Name of lead
        /// </summary>
        /// 

        [Display(Name = "Lead Name")]
        public int SelectedEmployee { get; set; }
        public IEnumerable<SelectListItem> ListEmployee { get; set; }

        public string LeadName { get; set; }

        /// <summary>
        /// Flag to know if have a project
        /// </summary>
        public bool HaveProject { get; set; }


    }
}