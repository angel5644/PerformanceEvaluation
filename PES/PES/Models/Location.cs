using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a location
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Location id
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// Location
        /// </summary>
        public string Name { get; set; }
    }
}