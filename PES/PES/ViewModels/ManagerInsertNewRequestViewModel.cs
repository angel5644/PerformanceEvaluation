using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PES.ViewModels
{
    public class ManagerInsertNewRequestViewModel
    {


        /// <summary>
        /// Employee Id
        /// </summary>  
        public int EmployeeId { get; set; }

        public string EmployeeEmail { get; set; }

        public string ManagerEmail { get; set; }
        /// <summary>
        /// Title of the request
        /// </summary>
        [Required(ErrorMessage = "Request Title is required")]
        public string Title { get; set; }

        /// <summary>
        /// Number of days requested 
        /// </summary>
        public int daysReq { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [Required(ErrorMessage = "Please submit a comment")]
        public string Comments { get; set; }

        /// <summary>
        /// Number of unpaid days
        /// </summary>
        public int? NoUnpaidDays { get; set; }

        /// <summary>
        /// List of New vacation date Viewmodel 
        /// </summary>
        public List<NewVacationDates> SubRequest { get; set; }

        // se agrego para provar el viewmodel completo// subrequest2 es el original
        public List<SubrequestInfoVM> SubRequest2 { get; set; }


        public IEnumerable<SelectListItem> ListEmployee { get; set; }

        /// <summary>
        /// Number of vacation days that employee have 
        /// </summary>
        public int Freedays { get; set; }

        //public string myFile { get; set;  }

        public VacHeadReqViewModel VacHeaderRequest { get; set; }

        //[DataType(DataType.Upload)]
        //HttpPostedFileBase myFile { get; set; }

    }
}