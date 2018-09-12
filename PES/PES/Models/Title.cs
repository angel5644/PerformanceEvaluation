using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a Title
    /// </summary>
    public class Title
    {
        /// <summary>
        /// Title Id
        /// </summary>  
        public int TitleId { get; set; }

        /// <summary>
        /// Title name
        /// </summary>
        public string Name { get; set; }
    }
}