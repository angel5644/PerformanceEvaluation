﻿using PES.Models;
using PES.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PES.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult latenessMenu()
        {
            LatenessService GetLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();
            lateness = GetLateness.GetEmployeesByManager((int)Session["UserId"]);

            return View(lateness);
        }
    }
}