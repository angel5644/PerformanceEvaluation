using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// This class will store a skill
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Skill Id
        /// </summary>
        public int SkillId {get; set;}

        /// <summary>
        /// Description of skill
        /// </summary>
        public string Description {get; set;}
    }
}