using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace PES.ViewModels
{
    [DataContract]
    public class Charts
    {
        [DataMember(Name = "label")]
        public string Month { get; set; }

        [DataMember(Name = "y")]
        public int VacationDays { get; set; }
    }
}