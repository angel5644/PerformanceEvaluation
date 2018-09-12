using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.ViewModels
{
    public class PEVisualizationViewModel
    {
        public PerformanceMainPartial PerformanceMain { get; set; }
        public PerformanceSectionsPartial PerformanceSections { get; set; }
        public PerformanceCommentsPartial PerformanceComments { get; set; }
        public PerformanceSkilsPartial PerformanceSkills { get; set; }
    }

    public class PerformanceMainPartial 
    {
        public Employee Employee { get; set; }
        public Employee Evaluator { get; set; }
        public double TotalEvaluation { get; set; }
    }

    public class PerformanceSectionsPartial
    {
        public List<PerformanceSectionHelper> Sections { get; set; }
    }

    public class PerformanceCommentsPartial
    {
        public List<Comment> Comments { get; set; }
    }

    public class PerformanceSkilsPartial 
    {
        public List<SkillHelper> Skills { get; set; }
    }
}