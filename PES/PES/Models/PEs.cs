using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PES.Models
{
    /// <summary>
    /// This class will store a Performance Evaluation
    /// </summary>
    public class PEs
    {
        /// <summary>
        /// Performance Evaluation Id
        /// </summary>
        public int PEId { get; set; }

        /// <summary>
        /// Performance Evaluation period
        /// </summary>
        public DateTime EvaluationPeriod { get; set; }

        /// <summary>
        /// Employee Id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Evaluator Id
        /// </summary>
        public int EvaluatorId { get; set; }

        /// <summary>
        /// PE Status Id
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Employee total score
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// English Score
        /// </summary>
        public double EnglishScore { get; set; }

        /// <summary>
        /// Performance Score
        /// </summary>
        public double PerformanceScore { get; set; }

        /// <summary>
        /// Competence Score
        /// </summary>
        public double CompeteneceScore { get; set; }

        /// <summary>
        /// Rank
        /// </summary>
        public double? Rank { get; set; }

        ///<sumary>
        /// Evaluation year
        /// </sumary>
        public int EvaluationYear { get; set; }

        ///<summary>
        ///Id Period
        ///</summary>
        public int PeriodId { get; set; }
    }

    public class UploadFileViewModel
    {
        public Employee CurrentUser { get; set; }
        public List<SelectListItem> ListEmployees { get; set; }
        public int SelectedEmployee { get; set; }
        public List<SelectListItem> ListAllEmployees { get; set; }
        public int SelectedEvaluator { get; set; }
        public List<SelectListItem> PeriodList { get; set; }
        public List<SelectListItem> ListYears { get; set; }
        public int SelectedPeriod { get; set; }
        public int SelectedYear { get; set; }
        public int Replace { get; set; }
    }

    public class HistoryViewModel
    {
        public int Period { get; set; }
        public int Year { get; set; }
    }
}