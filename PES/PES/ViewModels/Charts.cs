using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Newtonsoft.Json;


namespace PES.ViewModels
{
    [DataContract]
    public class Charts
    {
        [JsonProperty("label")]
        public string Month { get; set; }

        [JsonProperty("y")]
        public int VacationDays { get; set; }
    }
}