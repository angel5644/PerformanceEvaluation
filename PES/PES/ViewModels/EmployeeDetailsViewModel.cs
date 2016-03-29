﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.ViewModels
{
    public class EmployeeDetailsViewModel : Employee
    {
        public Employee Manager { get; set; }
        public Profile Profile { get; set; }
    }
}