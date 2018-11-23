using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.ViewModels
{
    public class ChartRequestsViewModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int VacationDays { get; set; }
    }
}