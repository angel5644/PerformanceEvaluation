using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store the dates of the vacation request
    /// </summary>
    public class VacationSubreq
    {
        /// <summary>
        /// Subrequest Id
        /// </summary>  
        public int SubreqId { get; set; }

        /// <summary>
        /// Vacation header request Id
        /// </summary>
        public int VacationHeaderReqId { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Return date
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Name of lead
        /// </summary>
        public string LeadName { get; set; }

        /// <summary>
        /// Flag to know if have a project
        /// </summary>
        public string HaveProject { get; set; }

    }
}