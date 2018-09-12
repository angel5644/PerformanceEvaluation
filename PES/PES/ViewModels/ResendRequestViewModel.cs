using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using PES.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace PES.ViewModels
{
    public class ResendRequestViewModel
    {
        //Atributes to get information from database. 
        public int RequestIdResend { get; set; }

        [Required(ErrorMessage = "Request Title is required")]
        public string TitleResend { get; set; } //Title

        public string StatusResend { get; set; }

        public int NovacDaysResend { get; set; }//daysReq

        public DateTime StartDateResend { get; set; }

        public DateTime EndDateResend { get; set; }

        public DateTime ReturnDateResend { get; set; }

        public string LeadNameResend { get; set; }

        //public string HaveProjectResend { get; set; }
        public bool HaveProjectResend { get; set; }

        [Required(ErrorMessage = "Please submit a comment")]
        public string CommentsResend { get; set; }

        public string ReplayCommentsResend { get; set; }

        public int FreeDaysResend { get; set; }

        public List<NewVacationDates> SubRequest { get; set; }

        public VacHeadReqViewModel VacHeaderRequest { get; set; }

        // userInformation 

        public int EmployeeId { get; set; }

        public string EmployeeEmail { get; set; }

        public string ManagerEmail { get; set; }

        public Employee ModelEmployeeResend { get; set; }


        //Not add  yet
        public int? NoUnpaidDays { get; set; }
        //Not add  yet

    }

}