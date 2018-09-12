using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public class LM_Skill
    {
        /// <summary>
        /// Evaluator Manager Skill Id
        /// </summary>
        public int LMSkillId { get; set; }

        public int SkillId { get; set; }

        /// <summary>
        /// Performance Evaluation Id
        /// </summary>
        public int PEId { get; set; }

        /// <summary>
        /// Property to store employee selection
        /// </summary>
        public string CheckEmployee { get; set; }

        /// <summary>
        /// Property to store evaluator selection
        /// </summary>
        public string CheckEvaluator { get; set; }

    }

    public class SkillHelper : LM_Skill
    {
        public string SkillName { get; set; }
    }
}