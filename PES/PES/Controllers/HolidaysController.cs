﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PES.Models;
using PES.Services;


namespace PES.Controllers
{
    public class HolidaysController : Controller
    {
        string MesCorrectoHoly;
        private HolidayService _holidayService;

        public HolidaysController()
        {
            _holidayService = new HolidayService();
        }

        // GET: Holidays1
        public ActionResult Index()
        {
            IEnumerable<Holiday> holidays = new List<Holiday>();
            holidays = _holidayService.GetAllHolidays();
            return View(holidays);
        }
        //Fix attribute insertDay, this has to be only the day attrirbute.
        [HttpGet]
        public ActionResult EditShow(int holidayId)
        {
            Holiday holiday = new Holiday();
            holiday =  _holidayService.GetHoliday(holidayId);
            string endDate = holiday.InsertDay;
            string eMonth = endDate.Substring(0, 2);
            string eDay = endDate.Substring(3, 2);
            string eYear = endDate.Substring(6, 4);
            string eFinalEndDate = (eDay + "/" + eMonth + "/" + eYear);
            holiday.InsertDay = eFinalEndDate;
            return View(holiday);     
        }
        [HttpPost]
        public ActionResult Edit(Holiday holiday)
        {
            string endDate = holiday.InsertDay;
            string eMonth = endDate.Substring(0, 2);
            string eDay = endDate.Substring(3, 2);
            string eYear = endDate.Substring(6, 4);
            string eFinalEndDate = (eDay + "/" + eMonth + "/" + eYear);
            holiday.InsertDay = eFinalEndDate;

            _holidayService.EditHoliday(holiday);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(Holiday model)
        {
            _holidayService.DeleteHoliday(model.HolidayId);
            return RedirectToAction("Index");
        }


        public ActionResult Create()
        {
                return View();            
        }
        // POST: Holidays1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Holiday holiday)
        {            
            Holiday newHoliday = new Holiday();
            newHoliday.Description = holiday.Description;
            string endDate = holiday.InsertDay;
            string eMonth = endDate.Substring(0, 2);
            switch (eMonth)
            {
                case "01":
                    {
                        MesCorrectoHoly = "JAN";
                        break;
                    }
                case "02":
                    {
                        MesCorrectoHoly = "FEB";
                        break;
                    }
                case "03":
                    {
                        MesCorrectoHoly = "MAR";
                        break;
                    }
                case "04":
                    {
                        MesCorrectoHoly = "APR";
                        break;
                    }
                case "05":
                    {
                        MesCorrectoHoly = "MAY";
                        break;
                    }
                case "06":
                    {
                        MesCorrectoHoly = "JUN";
                        break;
                    }
                case "07":
                    {
                        MesCorrectoHoly = "JUL";
                        break;
                    }
                case "08":
                    {
                        MesCorrectoHoly = "AUG";
                        break;
                    }
                case "09":
                    {
                        MesCorrectoHoly = "SEP";
                        break;
                    }
                case "10":
                    {
                        MesCorrectoHoly = "OCT";
                        break;
                    }
                case "11":
                    {
                        MesCorrectoHoly = "NOV";
                        break;
                    }
                case "12":
                    {
                        MesCorrectoHoly = "DEC";
                        break;
                    }
            }
            string eDay = endDate.Substring(3, 2);
            string eYear = endDate.Substring(6, 4);
            string eFinalEndDate = (eDay + "-" + MesCorrectoHoly + "-" + eYear);
            newHoliday.InsertDay = eFinalEndDate;
            if (ModelState.IsValid)
            {
                _holidayService.CreateHoliday(newHoliday);
            }
            return RedirectToAction("Create");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteHoliday(Holiday holiday)
        {

            return View();
        }

    }
}
