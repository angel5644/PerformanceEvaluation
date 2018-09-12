using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PES.ViewModels; 

namespace PES.Models
{
    /// <summary>
    /// This class will store a vacation header request
    /// </summary>
    public class VacationHeaderReq
    {
        /// <summary>
        /// Vacation header request Id
        /// </summary>  
        public int VacationHeaderReqId { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>  
        public int EmployeeId { get; set; }

        /// <summary>
        /// Title of the request
        /// </summary>
        [Required(ErrorMessage = "Request Title is required")]
        public string Title { get; set; }

        /// <summary>
        /// Number of vacation days
        /// </summary>
        public int NoVacDays { get; set; }

        /// <summary>
        /// Comments
        /// </summary>
        [Required(ErrorMessage = "Please submit a comment")]
        public string Comments { get; set; }

        /// <summary>
        /// Request status Id
        /// </summary>
        public int ReqStatusId { get; set; }

        /// <summary>
        /// Replay Comment
        /// </summary>
        [Required(ErrorMessage = "A cancel reason is required")]
        public string ReplayComment { get; set; }

        /// <summary>
        /// Number of unpaid days
        /// </summary>
        public int? NoUnpaidDays { get; set; }

        public StatusRequestViewModel Model { get; set; }
    }
    
}