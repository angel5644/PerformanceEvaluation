using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Web.Mvc;

namespace PES.ViewModels
{
    public class PerformanceFilesPartial
    {
        public IEnumerable<EmployeeManagerViewModel> listFiles { get; set; }
        public Employee CurrentUser { get; set; }
        public int? CountRankUpdated { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedPeriod { get; set; }
        public List<SelectListItem> ListYear { get; set; }
        public List<SelectListItem> ListPeriods { get; set; }

        public PerformanceFilesPartial(IEnumerable<EmployeeManagerViewModel> listFiles, Employee currentUser)
        {
            this.listFiles = listFiles;
            this.CurrentUser = currentUser;
            this.CountRankUpdated = null;
        }
    }
}