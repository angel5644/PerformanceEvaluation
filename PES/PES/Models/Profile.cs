using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a profile
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Profile Id
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Profile name
        /// </summary>
        public string Name { get; set; }

        /// Comment
    }
    public enum ProfileUser
    {
        None = 0,
        Resource = 1,
        Manager = 2,
        Director = 3
    }
}