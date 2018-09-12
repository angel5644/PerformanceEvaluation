using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PES.Models;
using PES.Services;
using PES.ViewModels;

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

            var MesCorrecto = new FechaViewModel();
            string eMonth = endDate.Substring(2, 2);
            string eDay = endDate.Substring(0, 1);
            string eYear = endDate.Substring(5, 4);
            string eFinalEndDate = (eDay + "/" + eMonth + "/" + eYear);
            holiday.InsertDay = eFinalEndDate;
            return View(holiday);   
            
        }
        [HttpPost]
        public ActionResult Edit(Holiday holiday)
        {
            string endDate = holiday.InsertDay;
            string eMonth = endDate.Substring(0, 2);
            var MesCorrecto2 = new FechaViewModel();
            MesCorrectoHoly = MesCorrecto2.CorregirMes(eMonth).ToString();
            string eDay = endDate.Substring(3, 2);
            string eYear = endDate.Substring(6, 4);
            string eFinalEndDate = (eDay + "/" + MesCorrectoHoly + "/" + eYear);
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

            var MesCorrecto = new FechaViewModel();
            string monthh = MesCorrecto.CorregirMes(eMonth).ToString();


            string eDay = endDate.Substring(3, 2);
            string eYear = endDate.Substring(6, 4);
            string eFinalEndDate = (eDay + "-" + monthh + "-" + eYear);
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
