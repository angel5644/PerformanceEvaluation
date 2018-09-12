using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a Description
    /// </summary>
    public class Description
    {
        /// <summary>
        /// Description Id
        /// </summary>
        public int DescriptionId { get; set; }

        /// <summary>
        /// Description 
        /// </summary>
        public string DescriptionText { get; set; }

        /// <summary>
        /// Subtitle Id
        /// </summary>
        public int SubtitleId { get; set; }
    }
}