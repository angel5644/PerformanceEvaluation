using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a vacation request status
    /// </summary>
    public class VacationReqStatus
    {
        /// <summary>
        /// Vacation request status Id
        /// </summary>
        public int ReqStatusId { get; set; }

        /// <summary>
        /// Vacation request status name
        /// </summary>
        public string Name { get; set; }
        
    }
}