using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.ViewModels
{
    public class ReadOnlyUserViewModel
    {
        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        public int EmployeeId { get; set; }

        public bool EmployeeProject { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; } 

        public DateTime ReturnDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string StatusOfRequest { get; set; }

        public string ResourceManager { get; set; }

        public int NumberOfDays { get; set; }

        public string TitleOfRequest { get; set; }

        public int RequestID { get; set; }

        public string RequestType { get; set; }
    }
}