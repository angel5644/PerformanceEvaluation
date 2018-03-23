﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.ViewModels
{
    public class SendRequestViewModel
    {
        public int EmployeedID { get; set; }

        public string Title { get; set; }

        public int DaysRequested { get; set; }

        public int VacationDays { get; set; }

        public  IEnumerable<Status> Status { get; set; }

        public string Comments { get; set; }

        public List<SubrequestInfoVM> SubRequests { get; set; }
    }
}