using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public class Lateness
    {
        public int LatenessId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int EmployeeId { get; set; }

        public String EmployeeEmail { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public String EmployeeName { get; set; }

        public int NoLateness { get; set; }

        public int Status { get; set; }
    }
}