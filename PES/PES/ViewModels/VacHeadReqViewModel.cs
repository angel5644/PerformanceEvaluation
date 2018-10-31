using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;


namespace PES.ViewModels
{
    public class VacHeadReqViewModel : VacationHeaderReq
    {
        /// <summary>
        /// Fist name of the employee
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Days requested 
        /// </summary>
        public int FreeDays { get; set; }

        /// <summary>
        /// Status of the Request
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Start Date of the Vacation Request
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End Date of the Vacation Request
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Return Date of the Employee's Vacations
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Creation Date of the Employee's Vacations
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Lead's Name of the employee in the period of the vacations
        /// </summary>
        public string LeadName { get; set; }

        //public bool HasLead { }

        /// <summary>
        /// Flag to know if the employee has a project
        /// </summary>
        public string HaveProject { get; set; }

        public StatusRequestViewModel Modal { get; set; }

        public int modalActive { get; set; }



    }

}