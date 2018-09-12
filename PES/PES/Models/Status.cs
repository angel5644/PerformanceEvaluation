using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a status
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Status Id
        /// </summary>  
        public int StatusId { get; set; }

        /// <summary>
        /// Status of the Performance Evaluation
        /// </summary>
        public string Description { get; set; }
    }
}