using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.ViewModels
{
    public class RequestDetailsViewModel : ReadOnlyUserViewModel
    {
        public string EmployeePosition { get; set; }

        public string EmployeeProfile { get; set; }

        public string EmployeeLocation { get; set; }

        public int EmployeeFreeDays { get; set; }

        public string EmployeeLeader { get; set; }

        public int ManagerID { get; set; }

        public string ManagerPosition { get; set; }

        public string Comments { get; set; }

        public int UnpaidDays { get; set; } = 0;
    }
}