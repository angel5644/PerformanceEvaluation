using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public class Score
    {
        /// <summary>
        /// Id Scores
        /// </summary>
        public int ScoreId { get; set; }

        public int DescriptionId { get; set; }

        /// <summary>
        /// Id Performance Evaluation
        /// </summary>
        public int PEId { get; set; }

        public int ScoreEmployee { get; set; }

        public int ScoreEvaluator { get; set; }

        /// <summary>
        /// Comments if the Scores are diferentes
        /// </summary>
        public string Comments { get; set; }

        public double Calculation { get; set; }      
        
    }
}