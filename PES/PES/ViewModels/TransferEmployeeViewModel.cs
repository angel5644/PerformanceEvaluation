using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;
using System.Web.Mvc;

namespace PES.ViewModels
{
    public class TransferEmployeeViewModel
    {
        public List<SelectListItem> ProfilesList { get; set; }
        public int SelectedProfile { get; set; }
        public List<SelectListItem> ManagerAList { get; set; }
        public Employee SelectedManagerA { get; set; }
        public List<SelectListItem> ManagerBList { get; set; }
        public Employee SelectedManagerB { get; set; }
        public List<Employee> ManagerEmployeeList { get; set; }
    }
}