using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        /// <summary>
        /// Id Performance Evaluation - Test commit
        /// </summary>
        public int PEId { get; set; }
            
        public string TrainningEmployee { get; set; }

        public string TrainningEvaluator { get; set; }

        public string AcknowledgeEvaluator { get; set; }

        public string CommRecommEmployee { get; set; }

        public string CommRecommEvaluator { get; set; }

    }
}