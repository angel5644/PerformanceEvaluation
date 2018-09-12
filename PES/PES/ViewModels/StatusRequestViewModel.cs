using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;


namespace PES.ViewModels
{    
    public class StatusRequestViewModel 
    {
        public int HeaderRequestId { get; set; }

        public string Title { get; set; }

        public string currentStatusId { get; set; }

        /// <summary>
        /// Replay comment or reason of cancelation
        /// </summary>
        public string Reason { get; set; }

        public string EmployeeEmail { get; set; }

        public string ManagerEmail { get; set; }

        public int NoVacDaysRequested { get; set; }

        public SendRequestViewModel VMSendRequest { get; set; }
    }
}