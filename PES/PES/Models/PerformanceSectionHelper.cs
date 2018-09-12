using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public class PerformanceSectionHelper
    {
        /// <summary>
        /// Get Performance/Competece Title 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get Performance/Competence Subtitles 
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Get Performance/Competence Descriptions
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Get Performance/Competence Score Employee
        /// </summary>
        public int ScoreEmployee { get; set; }

        /// <summary>
        /// Get Performance/Competence Score Evaluator 
        /// </summary>
        public int ScoreEvaluator { get; set; }

        /// <summary>
        /// Get Performance/Competence Comments 
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Get Performance/Competence Calculation
        /// </summary>
        public double Calculation { get; set; }

    }
}