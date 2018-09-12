using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a subtitle
    /// </summary>
    public class Subtitle
    {
        /// <summary>
        /// Subtitle Id
        /// </summary>  
        public int SubtitleId { get; set; }

        /// <summary>
        /// Subtitle name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Title Id
        /// </summary>  
        public int TitleId { get; set; }
    }
}