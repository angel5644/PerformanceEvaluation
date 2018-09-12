using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using PES.Models;
using PES.Services;
using PES.ViewModels;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Globalization;
using System.Threading;

namespace PES.Controllers
{
    public class LatenessController : Controller
    {
        private LatenessService getLateness; // _latenessService;

        public LatenessController()
        {
            getLateness = new LatenessService();
        }

        //Show the latenesses to the current user
        public ActionResult Index()
        {
            //LatenessService getLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            var userEmail = (string)Session["UserEmail"];
            //var nickName = getLateness.GetNickName(userEmail);

            lateness = getLateness.GetLatenessByEmail(userEmail, "today");
            return View(lateness);
        }

        //Get the lateness from period: today, week, month, year and last 5 years
        [HttpPost]
        public ActionResult GetLatenessByFilter(string period, string email)
        {
            //LatenessService getLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            ViewBag.period = period.ToUpper();

            if (email != null)
            {
                lateness = getLateness.GetLatenessByEmail(email, period);
                return PartialView("_LatenessReportsByUserPartial", lateness);
            }
            else
            {
                var currentUserEmail = (string)Session["UserEmail"];
                //var nickName = getLateness.GetNickName(currentUserEmail);
                lateness = getLateness.GetLatenessByEmail(currentUserEmail, period);
            }

            return PartialView("_LatenessReportsPartial", lateness);
        }

        //To show all the latenesses to manager (current month)
        public ActionResult LatenessAllUsers()
        {
            if ((int)Session["UserProfile"] != 2)
                return RedirectToAction("Index", "Menu");

            LatenessService getLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            lateness = getLateness.GetLatenessByCurrentMonth((int)Session["UserId"]);
            return View(lateness);
        }

        //Show the excel file imported by manager
        public ActionResult ShowLatenesExcel(List<Lateness> lateness)
        {
            if ((int)Session["UserProfile"] != 2)
                return RedirectToAction("Index", "Menu");

            return View(lateness);
        }

        //Read the excel file imported by manager
        [HttpPost]
        public ActionResult UploadLatenessExcel()
        {
            List<Lateness> latenesses = new List<Lateness>();
            DateTime minDate = DateTime.Today;
            DateTime maxDate = DateTime.Today;


            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));

                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var noOfCol = workSheet.Dimension.End.Column;
                        var noOfRow = workSheet.Dimension.End.Row;

                                             
                        long date = long.Parse(workSheet.Cells[2, 2].Value.ToString());
                        var tmp = DateTime.FromOADate(date);
                        minDate = tmp;
                        maxDate = tmp;

                       /*DateTime tmp = Convert.ToDateTime(workSheet.Cells[2, 2].Value.ToString());
                        minDate = Convert.ToDateTime(tmp.Month + "/" + tmp.Day + "/" + tmp.Year);
                        maxDate = Convert.ToDateTime(tmp.Month + "/" + tmp.Day + "/" + tmp.Year);*/

                        for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                        {
                           try
                           {
                                Lateness lateness = new Lateness();
                                lateness.EmployeeEmail = workSheet.Cells[rowIterator, 1].Value.ToString();

                                long tmpDate = long.Parse(workSheet.Cells[rowIterator, 2].Value.ToString());
                                lateness.Date = DateTime.FromOADate(tmpDate);
                            
                                double tmpTime = double.Parse(workSheet.Cells[rowIterator, 3].Value.ToString());
                                lateness.Time = DateTime.FromOADate(tmpTime);

                                latenesses.Add(lateness);

                                if (minDate > lateness.Date)
                                {
                                    minDate = lateness.Date;
                                }

                                if (maxDate < lateness.Date)
                                {
                                    maxDate = lateness.Date;
                                }
                           }
                           catch (Exception ex)
                           {
                              Console.Write(ex.Message);
                           }
                        }
                   }
                }

            }
            Session["tmpLateness"] = latenesses;
            ViewBag.sunday = minDate.Date.AddDays(-(int)minDate.Date.DayOfWeek).ToString("MM/dd/yyyy");
            ViewBag.saturday = maxDate.Date.AddDays(-(int)maxDate.Date.DayOfWeek + (int)DayOfWeek.Saturday).ToString("MM/dd/yyyy");
            Session["startWeek"] = minDate.Date.AddDays(-(int)minDate.Date.DayOfWeek).ToString("dd/MM/yy");
            Session["endWeek"] = maxDate.Date.AddDays(-(int)maxDate.Date.DayOfWeek + (int)DayOfWeek.Saturday).ToString("dd/MM/yy");

            return View("ShowLatenesExcel", latenesses);
        }

        //Save the excel file imported by manager
        [HttpPost]
        public string saveLatenessExcel(bool confirmReplace)
        {
            try
            {
                LatenessService lateness = new LatenessService();
                var latenessList = Session["tmpLateness"] as List<Lateness>;
                string nameNotFound = "";

                if (confirmReplace)
                {
                    nameNotFound = lateness.ReplaceExcel(latenessList, (string)Session["startWeek"], (string)Session["endWeek"]);
                }
                else if (!lateness.isExcelImported((string)Session["startWeek"], (string)Session["endWeek"]))
                {
                    nameNotFound = lateness.InsertLateness(latenessList);                 
                }
                else
                {
                    return "imported";
                }

                if (nameNotFound != "")
                    return "The file has been saved successfully, but <strong>" + nameNotFound + "</strong> were not found in the database";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "true";
        }

        //For the manager to see the list of lateness by user
        [HttpGet]
        public ActionResult LatenessByUser(string name, string email, string vname)
        {
            LatenessService getLateness = new LatenessService();
            List<Lateness> lateness = new List<Lateness>();

            ViewBag.name = vname;
            lateness = getLateness.GetLatenessByEmail(email, "today");
            return View(lateness);
        }

        //for the manager to delete
        [HttpPost]
        public bool LatenessLogicDelete(int id)
        {
            LatenessService LatenessDelete = new LatenessService();

            if (LatenessDelete.Delete(id))
            {
                return true;
            }

            return false;
        }

        //for the manager to cancel the justify
        [HttpPost]
        public bool LatenessLogicCancel(int id)
        {
            LatenessService latenessCancel = new LatenessService();

            if (latenessCancel.Cancel(id))
            {
                return true;
            }

            return false;
        }

        //get the nickname of employee
        //[HttpPost]
        //public string getNickName(string email)
        //{
        //    LatenessService lateness = new LatenessService();
        //    return lateness.GetNickName(email);   
        //}
    }
}