using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PES.Services;
using PES.Models;
using System.Threading.Tasks;
using PES.ViewModels;
// email 
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Web.Routing;
using System.Web.Optimization;
using System.IO;
using PES.Enums;

namespace PES.Controllers
{
    [Authorize]
    public class VacationRequestController : Controller
    {
        string MesCorrectoFinal;
        string MesCorrectoInicio;
        //Declare Services
        private EmployeeService _employeeService;
        private VacationHeaderReqService _headerReqService;
        private VacationReqStatusService _ReqStatusService;
        private VacationSubreqService _subReqService;
        private LocationService _locationService;
        //Added
        private HolidayService _holidayService;
        private EmailInsertNewRequestService _emailInsertNewRequestService;
        // status 
        private EmailCancelRequestService _emailCancelRequestService;
        private EmailApproveRequestService _emailApproveRequestService;
        private EmailRejectRequestService _emailRejectRequestService;
        private EmailResendRequestService _emailResendRequestService;
        // private EmailService 
        private ResendRequestService _resendRequestService;


        public VacationRequestController()
        {
            _employeeService = new EmployeeService();
            _headerReqService = new VacationHeaderReqService();
            _ReqStatusService = new VacationReqStatusService();
            _subReqService = new VacationSubreqService();
            //Added
            _holidayService = new HolidayService();
            _emailCancelRequestService = new EmailCancelRequestService();
            _emailInsertNewRequestService = new EmailInsertNewRequestService();
            _emailApproveRequestService = new EmailApproveRequestService();
            _emailRejectRequestService = new EmailRejectRequestService();
            _resendRequestService = new ResendRequestService();
            _emailResendRequestService = new EmailResendRequestService();
            _locationService = new LocationService();
        }

        /// <summary>
        /// GET: New vacation requests. The metod only need a User Id parameter 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>New Request Screen</returns>
        [HttpGet]
        public ActionResult InsertNewRequest(int userid)
        {
            Employee currentEmployee = new Employee();
            _employeeService = new EmployeeService();
            currentEmployee = _employeeService.GetByID(userid);
            InsertNewRequestViewModel newRequest = new InsertNewRequestViewModel();
            newRequest.EmployeeId = userid;
            newRequest.Freedays = currentEmployee.Freedays;
            newRequest.SubRequest = new List<NewVacationDates>()
            {
                new NewVacationDates
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    HaveProject = true
                }
            };
            ViewBag.newRequest = userid;
            ViewBag.MyHoliday = new HolidayService().GetAllHolidays();
            return View(newRequest);
        }

        [HttpGet]
        public ActionResult SendRequest(int userid)
        {
            Employee currentEmployee = _employeeService.GetByID(userid);

            if(currentEmployee.Freedays == 0)
            {
                return RedirectToAction("HistoricalResource");
            }

            if (currentEmployee.EmployeeId > 0)
            {
                // Is valid
                SendRequestViewModel NewRequest = new SendRequestViewModel();

                // mod
                IEnumerable<Employee> listEmployee = new List<Employee>();
                listEmployee = _employeeService.GetAll();

                IEnumerable<SelectListItem> listEmployees = listEmployee.Select(employee => new SelectListItem()
                {
                    Text = employee.FirstName + " " + employee.LastName,
                    Value = employee.EmployeeId.ToString()
                });
                //mod

                NewRequest.TypeRequest = 0;
                NewRequest.EmployeedID = userid;
                NewRequest.VacationDays = currentEmployee.Freedays;
                NewRequest.SubRequests = new List<SubrequestInfoVM>()
                {
                    new SubrequestInfoVM
                    {
                        ListEmployee = listEmployees,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddDays(1)
                    }
                };
                ViewBag.NewRequest = userid;
                ViewBag.MyHoliday = new HolidayService().GetAllHolidays();
                return View(NewRequest);
            }
            else
            {
                TempData["Error"] = "User was not found";
                return RedirectToAction("HistoricalResource", "VacationRequest");
            }


        }



        // new send request for unpaid vacations
        [HttpGet]
        public ActionResult SendRequestUnpaid(int id)
        {

            Employee CurrentEmployee = new Employee();
            _employeeService = new EmployeeService();
            CurrentEmployee = _employeeService.GetByID(id);
            SendRequestViewModel NewRequest = new SendRequestViewModel();

            // mod
            IEnumerable<Employee> listEmployee = new List<Employee>();
            listEmployee = _employeeService.GetAll();

            IEnumerable<SelectListItem> listEmployees = listEmployee.Select(employee => new SelectListItem()
            {
                Text = employee.FirstName + " " + employee.LastName,
                Value = employee.EmployeeId.ToString()
            });
            //mod
            //NewRequest.TypeRequest = 1;
            NewRequest.TypeRequest = Convert.ToInt32(RequestType.IsUnpaid);
            NewRequest.EmployeedID = id;
            NewRequest.VacationDays = CurrentEmployee.Freedays;
            NewRequest.SubRequests = new List<SubrequestInfoVM>()
            {
                new SubrequestInfoVM
                {
                    ListEmployee = listEmployees,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    HaveProject = false
                }
            };
            ViewBag.NewRequest = id;
            ViewBag.MyHoliday = new HolidayService().GetAllHolidays();
            return View(NewRequest);

        }


        [HttpGet]
        public ActionResult EmergencyRequest(int id)
        {

            

            Employee CurrentEmployee = new Employee();
            _employeeService = new EmployeeService();
            CurrentEmployee = _employeeService.GetByID(id);
            SendRequestViewModel NewRequest = new SendRequestViewModel();

            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "Funeral",
                Value = "3"
            });
            listItems.Add(new SelectListItem
            {
                Text = "Paternity",
                Value = "4",
            });
            listItems.Add(new SelectListItem
            {
                Text = "Other Emergency",
                Value = "2",
            });
        

            IEnumerable<Employee> listEmployee = new List<Employee>();
            listEmployee = _employeeService.GetAll();

            IEnumerable<SelectListItem> listEmployees = listEmployee.Select(employee => new SelectListItem()
            {
                Text = employee.FirstName + " " + employee.LastName,
                Value = employee.EmployeeId.ToString()
            });
            //mod
            NewRequest.ListRequestType = listItems;
          //  NewRequest.TypeRequest = Convert.ToInt32(RequestType.UnEmergency);
            NewRequest.EmployeedID = id;
            NewRequest.VacationDays = CurrentEmployee.Freedays;
            NewRequest.SubRequests = new List<SubrequestInfoVM>()
            {
                new SubrequestInfoVM
                {
                    ListEmployee = listEmployees,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    HaveProject = false
                }
            };
            ViewBag.NewRequest = id;
            ViewBag.MyHoliday = new HolidayService().GetAllHolidays();
            return View(NewRequest);

        }


        public ActionResult VacationAssignation()
        {

            //******************
          //  IEnumerable<Employee> listEmployee = new List<Employee>();
            //List<Employee> listEmployeeFiltered = new List<Employee>();
            //listEmployee = _employeeService.GetAll();

            // NOTE: add more attributes to the "var employeed" if you need them. 
         /*   foreach (var employ in listEmployee)
           {
                if (employ.Freedays != 0)
                {
                    var employeed = new Employee
                    {
                        EmployeeId = employ.EmployeeId,
                        Freedays = employ.Freedays,
                        FirstName = employ.FirstName,
                        LastName = employ.LastName,
                        Email = employ.Email,
                        ReminderDate = employ.ReminderDate

                    };

                    listEmployeeFiltered.Add(employeed);
                }
            }
            */
            //]]]]]]]]]]]]]]]]]]


            AssignVacationsViewModel AssignVacations = new AssignVacationsViewModel();

            IEnumerable<Employee> listEmployee = new List<Employee>();
            List<Employee> FilteredListEmployee = new List<Employee>();

            listEmployee = _employeeService.GetAll();

            foreach (var employ in listEmployee)
            {
                if (employ.Freedays != 0)
                {
                    FilteredListEmployee.Add(employ);                
                }
            }
            

            IEnumerable<SelectListItem> listEmployees = FilteredListEmployee.Select(employee => new SelectListItem()
            {
                Text = employee.FirstName + " " + employee.LastName,
                Value = employee.EmployeeId.ToString()
            });

            AssignVacations.ListEmployee = listEmployees;

            return View(AssignVacations);
        }




        public ActionResult ResendRequest(int headerReqId, int userid)
        {
            //Obtaing UserInformation 
            Employee currentEmployee = new Employee();
            _employeeService = new EmployeeService();
            currentEmployee = _employeeService.GetByID(userid);

            ResendRequestViewModel model = new ResendRequestViewModel();

            model = _resendRequestService.GetRequestInformation(headerReqId);
            model.ModelEmployeeResend = currentEmployee;
            ViewBag.MyHoliday = new HolidayService().GetAllHolidays();
            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateResendRequest(ResendRequestViewModel model)
        {
            //Update by  Header Request.
            _resendRequestService.UpdateResendRequestHeaderReq(model);
            //Obtain id header request inserted.

            string[] StartAndEndate;

            for (int i = 0; i < model.SubRequest.Count(); i++)
            {
                StartAndEndate = model.SubRequest[i].Date.Split('-');
                //Changing date format.
                string startDate = StartAndEndate[0].Trim();
                string month = startDate.Substring(0, 2);
                string day = startDate.Substring(3, 2);
                string year = startDate.Substring(6, 4);
                string finalStarDate = (day + "/" + month + "/" + year);
                //Changing date format.
                string endDate = StartAndEndate[1].Trim();
                string eMonth = endDate.Substring(0, 2);
                string eDay = endDate.Substring(3, 2);
                string eYear = endDate.Substring(6, 4);
                string eFinalEndDate = (eDay + "/" + eMonth + "/" + eYear);
                //Sending Information to ViewModel.
                model.SubRequest[i].StartDate = Convert.ToDateTime(finalStarDate.Trim());
                model.SubRequest[i].EndDate = Convert.ToDateTime(eFinalEndDate.Trim());

            }
            //inserting sub request.
            _resendRequestService.UpdateResendRequestSubReq(model.RequestIdResend, model.SubRequest);


            List<ResendRequestViewModel> data = new List<ResendRequestViewModel>();
            data = _emailResendRequestService.GetEmail(model.RequestIdResend);
            string employeeEmail = data[0].EmployeeEmail;
            string managerEmail = data[0].ManagerEmail;
            List<string> emails = new List<string>()
            {
                employeeEmail,
                managerEmail
            };
            _emailResendRequestService.SendEmails(emails, "Resend Vacation Request ", model.CommentsResend /*, model.myFile*/);
            _emailResendRequestService.Lessnovacdays(employeeEmail, model.NovacDaysResend);

            //return to History View.
            return RedirectToAction("HistoricalResource");
        }


        /// <summary>
        /// POST of New Vacation Request to get all data form the New Request View to process and insert them in the DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Redirect to Historial Screen</returns>
        [HttpPost]
        public ActionResult InsertNewRequestData(SendRequestViewModel model)
        {
            //// Validate if unpaid days will be taken, return unpaid days that will be taken
            //int unpaidDays = 0;
            // Send normal request with vacation days
            #region normal vacation days request
            //int vacationDays = model.daysReq - unpaidDays;
            //model.daysReq = vacationDays;

            //model.SubRequests.
            if(model.TypeRequest == 0)
            {
                var FirstElement = model.SubRequests.First();
                DateTime nowa = DateTime.Now;
                var weekNumberfive = nowa.AddDays(35);
                if (FirstElement.StartDate.Date < weekNumberfive.Date)
                {
                    model.TypeRequest = 6;
                }
            }


            //Insert Header Request.
            if (model.TypeRequest == 0 || model.TypeRequest == 6)
            {
                InsertRequest(model);
            }
               else if(model.TypeRequest == 5)
            {
                InsertAssigntRequest(model);
            }
            else
            {
                InsertRequestSpecial(model);
            }
            




            #endregion

            //if (unpaidDays > 0)
            //{
            //    // Send unpad days request
            //    model.daysReq = unpaidDays;
            //    model.TypeRequest = 1;

            //    InsertRequest(model);
            //}


            //return to History View.
            return RedirectToAction("HistoricalResource");
        }

        // working on ////

        private bool InsertRequestSpecial(SendRequestViewModel model)
        {

            Employee employee = _employeeService.GetByID(model.EmployeedID);

            // Get manager 
            Employee manager = _employeeService.GetByID(employee.ManagerId);

            // Emails where the request will be sent to
            List<string> emails = new List<string> { employee.Email, manager.Email };

            foreach (var subrequest in model.SubRequests)
            {
                _headerReqService.InsertVacHeaderReq(model);


                int idRequest = _subReqService.GetHeaderRequest(model);


                _subReqService.InsertSubrequest(idRequest, subrequest);

                _emailInsertNewRequestService.SendEmails(emails, "New Vacation Request ", model.Comments, model.MyFile);


                _emailInsertNewRequestService.UpdateVacationDays(employee.Email, model.daysReq, model.TypeRequest);

            }

                    return true;

        }


        private bool InsertAssigntRequest(SendRequestViewModel model)
        {
            model.TypeRequest = 5;

            Employee employee = _employeeService.GetByID(model.EmployeedID);

            // Get manager 
            Employee manager = _employeeService.GetByID(employee.ManagerId);

            // Emails where the request will be sent to
            List<string> emails = new List<string> { employee.Email, manager.Email };

            foreach (var subrequest in model.SubRequests)
            {
                _headerReqService.InsertVacHeaderReq(model);


                int idRequest = _subReqService.GetHeaderRequest(model);


                _subReqService.InsertSubrequest(idRequest, subrequest);

                _emailInsertNewRequestService.SendEmails(emails, "New Vacation Request ", model.Comments, model.MyFile);


                _emailInsertNewRequestService.UpdateVacationDays(employee.Email, model.daysReq, model.TypeRequest);

            }

            return true;

        }

        private bool InsertRequest(SendRequestViewModel model)
        {
            ////Insert Header Request.
            //_headerReqService.InsertVacHeaderReq(model);

            ////Obtain id header request inserted.
            //int idRequest = _subReqService.GetHeaderRequest(model);
            int daysRequestedForUnpaid = model.daysReq;
            int vacationsForThisEmploeForUnpdaid = model.VacationDays;
            ////inserting sub request.
            //_subReqService.InsertSubReq(idRequest, model.SubRequests);

            // Get employee
            Employee employee = _employeeService.GetByID(model.EmployeedID);

            // Get manager 
            Employee manager = _employeeService.GetByID(employee.ManagerId);

            // Emails where the request will be sent to
            List<string> emails = new List<string> { employee.Email, manager.Email };

            int vacationDays = employee.Freedays;

            #region insert request for every subrequest
            foreach (var subrequest in model.SubRequests)
            {
                // Update number of vacation days
                vacationDays = _employeeService.GetByID(employee.EmployeeId).Freedays;
                //model.daysReq = (subrequest.EndDate.Date - subrequest.StartDate.Date).Days + 1;

                if (model.daysReq <= vacationDays)
                {
                    // insert request in db
                    _headerReqService.InsertVacHeaderReq(model);

                    // get header request id
                    int idRequest = _subReqService.GetHeaderRequest(model);

                    //inserting sub request.
                    _subReqService.InsertSubrequest(idRequest, subrequest);

                    _emailInsertNewRequestService.SendEmails(emails, "New Vacation Request ", model.Comments, model.MyFile);

                    // Update 
                    _emailInsertNewRequestService.UpdateVacationDays(employee.Email, model.daysReq, model.TypeRequest);
                }
                else
                {
                    // We know that unpaid days will be needed

                    // Send normal request up to the vacation days
                    int normalReqDays = vacationDays;
                    bool sendNormalRequest = normalReqDays > 0;

                    if (sendNormalRequest)
                    {
                        if (normalReqDays == 1)
                        {
                            subrequest.EndDate = subrequest.StartDate.Date.AddDays(normalReqDays - 1);
                            if (subrequest.EndDate.DayOfWeek == DayOfWeek.Saturday)
                            {
                                subrequest.EndDate = subrequest.EndDate.Date.AddDays(2);
                            }
                            else if (subrequest.EndDate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                subrequest.EndDate = subrequest.EndDate.Date.AddDays(1);
                            }
                        }
                        else
                        {
                            var SumaTotal = 0;
                            subrequest.EndDate = subrequest.StartDate.Date;
                            while (true)
                            {
                                if (SumaTotal != normalReqDays -1)
                                {
                                    subrequest.EndDate = subrequest.EndDate.Date.AddDays(1);
                                    if (subrequest.EndDate.DayOfWeek == DayOfWeek.Saturday)
                                    {
                                        SumaTotal--;
                                    }
                                    else if (subrequest.EndDate.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        SumaTotal--;
                                    }
                                    SumaTotal++;
                                }
                                else
                                {
                                    break;
                                }

                            }

                        }


                        DateTime returDateDate = new DateTime();

                        returDateDate = subrequest.EndDate.Date.AddDays(1);
                        if (returDateDate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            returDateDate = returDateDate.Date.AddDays(2);
                        }
                        else if (returDateDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            returDateDate = returDateDate.Date.AddDays(1);
                        }

                        var shortDate = returDateDate.ToString("MM/dd/yyyy");

                        subrequest.ReturnDate = shortDate;

                        model.daysReq = normalReqDays;

                        // Send normal request

                        // insert request in db
                        _headerReqService.InsertVacHeaderReq(model);

                        // get header request id
                        int idRequest = _subReqService.GetHeaderRequest(model);

                        //inserting sub request.
                        _subReqService.InsertSubrequest(idRequest, subrequest);

                        _emailInsertNewRequestService.SendEmails(emails, "New Vacation Request ", model.Comments, model.MyFile);

                        // Update 
                        _emailInsertNewRequestService.UpdateVacationDays(employee.Email, model.daysReq, model.TypeRequest);
                    }

                    // Send unpaid request
                    int unpaidReqDays = daysRequestedForUnpaid - vacationsForThisEmploeForUnpdaid;

               

                    if (unpaidReqDays > 0)
                    {
                        subrequest.StartDate = subrequest.EndDate.Date.AddDays(1);
                        if (subrequest.StartDate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            subrequest.StartDate = subrequest.StartDate.Date.AddDays(2);
                        }
                        else if (subrequest.StartDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            subrequest.StartDate = subrequest.StartDate.Date.AddDays(1);
                        }

                        if(unpaidReqDays == 1)
                        {
                            subrequest.EndDate = subrequest.StartDate.Date.AddDays(unpaidReqDays - 1);
                            if (subrequest.EndDate.DayOfWeek == DayOfWeek.Saturday)
                            {
                                subrequest.EndDate = subrequest.EndDate.Date.AddDays(2);
                            }
                            else if (subrequest.EndDate.DayOfWeek == DayOfWeek.Sunday)
                            {
                                subrequest.EndDate = subrequest.EndDate.Date.AddDays(1);
                            }
                        }
                        else
                        {
                            var SumaTotal = 0;
                            subrequest.EndDate = subrequest.StartDate.Date;
                            while (true)
                            {
                                if(SumaTotal != unpaidReqDays -1)
                                {
                                    subrequest.EndDate = subrequest.EndDate.Date.AddDays(1);
                                    if (subrequest.EndDate.DayOfWeek == DayOfWeek.Saturday)
                                    {
                                        SumaTotal--;
                                    }
                                    else if (subrequest.EndDate.DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        SumaTotal--;
                                    }
                                    SumaTotal++;
                                }
                                else
                                {
                                    break;
                                }
                                
                            }
                            
                        }


                        DateTime returDateDate = new DateTime();

                        returDateDate = subrequest.EndDate.Date.AddDays(1);
                        if (returDateDate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            returDateDate = returDateDate.Date.AddDays(2);
                        }
                        else if (returDateDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            returDateDate = returDateDate.Date.AddDays(1);
                        }

                        var shortDate = returDateDate.ToString("MM/dd/yyyy");

                        subrequest.ReturnDate = shortDate;



                        model.daysReq = unpaidReqDays;
                        model.TypeRequest = 1;

                        // Send normal request

                        // insert request in db
                        _headerReqService.InsertVacHeaderReq(model);

                        // get header request id
                        int idRequest = _subReqService.GetHeaderRequest(model);

                        //inserting sub request.
                        _subReqService.InsertSubrequest(idRequest, subrequest);

                        _emailInsertNewRequestService.SendEmails(emails, "New Unpaid Vacation Request", model.Comments, model.MyFile);

                        // Update 
                        //_emailInsertNewRequestService.UpdateVacationDays(employee.Email, model.daysReq, model.TypeRequest);
                    }
                }
                
            }
            #endregion

            

            

            

            

            

            return true;
        }

		[HttpPost]
		public ActionResult ManagerInsertNewRequest(int SelectedEmployee)
		{


			Employee currentEmployee = _employeeService.GetByID(SelectedEmployee);

			SendRequestViewModel newRequest = new SendRequestViewModel();
			newRequest.EmployeedID = SelectedEmployee;
			newRequest.VacationDays = currentEmployee.Freedays;
			newRequest.FirstName = currentEmployee.FirstName;
			newRequest.LastName = currentEmployee.LastName;
            newRequest.TypeRequest = 5;
			newRequest.SubRequests = new List<SubrequestInfoVM>()
			{
				new SubrequestInfoVM
				{
					StartDate = DateTime.Now,
					EndDate = DateTime.Now.AddDays(1),
					HaveProject = true
				}
			};
			//agregar al view model reemplasar viewbag
			ViewBag.newRequest = SelectedEmployee;
			ViewBag.MyHoliday = _holidayService.GetAllHolidays();

			return View(newRequest);
		}
		/// <summary>
		/// GET: VacationRequest Existing
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public ActionResult EditHolidays(int userid)
		{
			return View();
		}

		/// <summary>
		/// GET: VacationRequest Existing. Metod to get a existing request using Header Request Id 
		/// </summary>
		/// <param name="headerReqId"></param>
		/// <returns>Redirect Vacation Request passing the data of the existing request</returns>
		[HttpGet]
		public ActionResult GetVacationRequest(int headerReqId)
		{
			VacHeadReqViewModel currentRequest = new VacHeadReqViewModel();
			currentRequest = _headerReqService.GetAllVacRequestInfoByVacReqId(headerReqId);
			Employee currentUser = new Employee();
			currentUser = _employeeService.GetByID(currentRequest.EmployeeId);

            
            ViewBag.status = currentRequest.status;
            currentRequest.FreeDays = currentUser.Freedays;
            currentRequest.FirstName = currentUser.FirstName;
            currentRequest.LastName = currentUser.LastName;
            currentRequest.Modal = new StatusRequestViewModel()
            {
                HeaderRequestId = currentRequest.VacationHeaderReqId,
                NoVacDaysRequested = currentRequest.NoVacDays,
                currentStatusId = currentRequest.status,
                Title = currentRequest.Title
                
                
            };

            //currentRequest.Modal = new StatusRequestViewModel()
            //{
            //    Title = currentRequest.Title
            //};


			return View("VacationRequest", currentRequest);
		}


        [HttpGet]
        public ActionResult SendIdNav(int TypeRedirectRequest)
        {

            Employee currentUser = new Employee();
            var currentUserEmail = (string)Session["UserEmail"];
            currentUser = _employeeService.GetByEmail(currentUserEmail);

            if(TypeRedirectRequest == 1)
            {
                return RedirectToAction("SendRequest", "VacationRequest", new { userid = currentUser.EmployeeId });
            }
            else if(TypeRedirectRequest == 2)
            {
                return RedirectToAction("SendRequestUnpaid", "VacationRequest", new { id = currentUser.EmployeeId });
            }
            else if(TypeRedirectRequest == 3)
            {
                return RedirectToAction("EmergencyRequest", "VacationRequest", new { id = currentUser.EmployeeId });
            }
            else
            {
                return RedirectToAction("EmergencyRequest", "VacationRequest", new { id = currentUser.EmployeeId });
            }

          



        }

		/// <summary>
		/// GET: HistoricalResource. Metod to Get all the data of the current employee and all its vacation requests 
		/// </summary>
		/// <returns>A list of all Resquests in the Historical Resource View</returns>
		[HttpGet]
		public ActionResult HistoricalResource()
		{

			//Get current user
			Employee currentUser = new Employee();
			var currentUserEmail = (string)Session["UserEmail"];
			currentUser = _employeeService.GetByEmail(currentUserEmail);


			List<VacHeadReqViewModel> listHeaderReqDB = new List<VacHeadReqViewModel>();
			List<VacHeadReqViewModel> listHeaderReqVM = new List<VacHeadReqViewModel>();


			if (currentUser.ProfileId == Convert.ToInt32(ProfileUser.Manager))
			{
				listHeaderReqDB = _headerReqService.GetAllGeneralVacationHeaderReqByManagerId(currentUser.EmployeeId);
			}
			else if (currentUser.ProfileId == Convert.ToInt32(ProfileUser.Resource))
			{
                listHeaderReqDB = _headerReqService.GetGeneralVacationHeaderReqByEmployeeId(currentUser.EmployeeId);
			}
			if (listHeaderReqDB != null && listHeaderReqDB.Count > 0)
			{
				foreach (var headerReq in listHeaderReqDB)
				{
					var headerReqVm = new VacHeadReqViewModel
					{
						VacationHeaderReqId = headerReq.VacationHeaderReqId,
						EmployeeId = headerReq.EmployeeId,
						Title = headerReq.Title,
						NoVacDays = headerReq.NoVacDays,
						status = _ReqStatusService.GetVacationReqStatusById(headerReq.ReqStatusId).Name,
						StartDate = headerReq.StartDate,
						EndDate = headerReq.EndDate,
						ReturnDate = headerReq.ReturnDate,
						FirstName = headerReq.FirstName,
						LastName = headerReq.LastName,
						HaveProject = headerReq.HaveProject,


					};
					listHeaderReqVM.Add(headerReqVm);
				}
			}

			ViewBag.Username = currentUser.FirstName + " " + currentUser.LastName;
			ViewBag.UserID = currentUser.EmployeeId;
			ViewBag.FreeDays = currentUser.Freedays;
			ViewBag.profileId = currentUser.ProfileId;
			ViewBag.LeadName = _subReqService.GetVacationSubreqByHeaderReqId(currentUser.EmployeeId);


			return View(listHeaderReqVM);
		} 
		


		public ActionResult VacationsReminder()
		{


			IEnumerable<Employee> listEmployee = new List<Employee>();
			List<Employee> listEmployeeFiltered = new List<Employee>();
			listEmployee = _employeeService.GetAll();

			// NOTE: add more attributes to the "var employeed" if you need them. 
			foreach (var employ in listEmployee)
			{
				if (employ.Freedays != 0)
				{
					var employeed = new Employee
					{
						EmployeeId = employ.EmployeeId,
						Freedays = employ.Freedays,
						FirstName = employ.FirstName,
						LastName = employ.LastName,
						Email = employ.Email,    
						ReminderDate = employ.ReminderDate
						
					};

					listEmployeeFiltered.Add(employeed);
				}
			}    

			return View(listEmployeeFiltered);
		}


        [HttpGet]
        public ActionResult VacationsReminderFilter(int testChar)
        {


            IEnumerable<Employee> listEmployee = new List<Employee>();
            List<Employee> listEmployeeFiltered = new List<Employee>();
            listEmployee = _employeeService.GetAll();

            // NOTE: add more attributes to the "var employeed" if you need them. 
            foreach (var employ in listEmployee)
            {
                if (employ.Freedays == testChar)
                {
                    var employeed = new Employee
                    {
                        EmployeeId = employ.EmployeeId,
                        Freedays = employ.Freedays,
                        FirstName = employ.FirstName,
                        LastName = employ.LastName,
                        Email = employ.Email,
                        ReminderDate = employ.ReminderDate

                    };

                    listEmployeeFiltered.Add(employeed);
                }
            }


            return PartialView("~/Views/Shared/_TableVacationRemainder.cshtml", listEmployeeFiltered);

        }



        [HttpGet]
        public ActionResult ResetVacationRemainder(bool testChar)
        {


            IEnumerable<Employee> listEmployee = new List<Employee>();
            List<Employee> listEmployeeFiltered = new List<Employee>();
            listEmployee = _employeeService.GetAll();

            // NOTE: add more attributes to the "var employeed" if you need them. 
            foreach (var employ in listEmployee)
            {
                if (employ.Freedays != 0)
                {
                    var employeed = new Employee
                    {
                        EmployeeId = employ.EmployeeId,
                        Freedays = employ.Freedays,
                        FirstName = employ.FirstName,
                        LastName = employ.LastName,
                        Email = employ.Email,
                        ReminderDate = employ.ReminderDate

                    };

                    listEmployeeFiltered.Add(employeed);
                }
            }


            return PartialView("~/Views/Shared/_TableVacationRemainder.cshtml", listEmployeeFiltered);

        }

        public JsonResult SendReminderEmail(int userid)
		{

            
			Employee RemindedEmployee = new Employee();
			RemindedEmployee = _employeeService.GetByID(userid);
			RemindedEmployee.ReminderDate = DateTime.Now;
            //_employeeService.InsertReminderEmployee(RemindedEmployee);
            _employeeService.UpdateEmployeeRemindedDate(RemindedEmployee);
			int vacationDays = RemindedEmployee.Freedays;
			string employeeEmail = RemindedEmployee.Email;
			//this is the hardcoded message, this should be editable?. need to ask
			string bodyEmail = "I have to remind you that you have " + vacationDays + " vacation days available, if you don't take them soon i might have to assign you vacations \nThis is not an automatic email, if you have any doubt feel free to ask me" ;

		   if( _emailInsertNewRequestService.SendEmail(employeeEmail, "Reminder From HR", bodyEmail))
			{
				return Json(true, JsonRequestBehavior.AllowGet);
			}

			return Json(false, JsonRequestBehavior.AllowGet);

		}

        public JsonResult SendReminderEmailToAll(int number)
        {
            //
            IEnumerable<Employee> listEmployee = new List<Employee>();
            List<Employee> listEmployeeFiltered = new List<Employee>();
            listEmployee = _employeeService.GetAll();

            // NOTE: add more attributes to the "var employeed" if you need them. 
            foreach (var employ in listEmployee)
            {
                if (employ.Freedays == number)
                {
                    var employeed = new Employee
                    {
                        EmployeeId = employ.EmployeeId,
                        Freedays = employ.Freedays,
                        FirstName = employ.FirstName,
                        LastName = employ.LastName,
                        Email = employ.Email,
                        ReminderDate = employ.ReminderDate

                    };

                    listEmployeeFiltered.Add(employeed);
                }
            }
            //
            int numberOfItems = listEmployeeFiltered.Count;
            int counterForEach = 0;
            int counterFailureSend = 0;
            foreach (var employ in listEmployeeFiltered)
            {
                Employee RemindedEmployee = new Employee();
                RemindedEmployee = _employeeService.GetByID(employ.EmployeeId);
                RemindedEmployee.ReminderDate = DateTime.Now;
                //_employeeService.InsertReminderEmployee(RemindedEmployee);
                _employeeService.UpdateEmployeeRemindedDate(RemindedEmployee);
                int vacationDays = RemindedEmployee.Freedays;
                string employeeEmail = RemindedEmployee.Email;
                //this is the hardcoded message, this should be editable?. need to ask
                string bodyEmail = "I have to remind you that you have " + vacationDays + " vacation days available, if you don't take them soon i might have to assign you vacations \nThis is not an automatic email, if you have any doubt feel free to ask me";

                if ( _emailInsertNewRequestService.SendEmail(employeeEmail, "Reminder From HR", bodyEmail))
                {
                    counterForEach++;
                }
                else
                {
                    counterFailureSend++;
                }
            }
            if (numberOfItems == counterForEach)
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
        }


        public JsonResult SendConfirmationHR(int userid,int VacationDays, string Sfechas)
		{   
			Employee RemindedEmployee = new Employee();
			RemindedEmployee = _employeeService.GetByID(userid);
			string employeeEmail = RemindedEmployee.Email;
			string wholeName = RemindedEmployee.FirstName + " " + RemindedEmployee.LastName;           
			Location UserLocation = _locationService.GetPeriodById(RemindedEmployee.LocationId);
			string locationName = UserLocation.Name;
			string employeeClient = RemindedEmployee.Customer;            

			string bodyEmail = "An employee has solicited vacations, can you please confirm me your aproval or rejection please. this is his information:\n" +
				"\n" +
				"Complete Name: " + wholeName + "\n" +
				"Location: " + locationName + "\n" +
				"Client: " + employeeClient + "\n" +
				"Dates Requested: " + Sfechas + "\n" +
				"Total Days Requested: " + VacationDays + "\n" +
				"\n" +
				"If you have any questions, feel free to ask me.";
		   
			if (_emailInsertNewRequestService.SendEmail("victor.munguia@4thsource.com", "Vacation Aproval", bodyEmail))
			{
				return Json(true, JsonRequestBehavior.AllowGet);
			}

			return Json(false, JsonRequestBehavior.AllowGet);

		}




		[HttpPost]
		public ActionResult CancelRequest(StatusRequestViewModel model)
		{
            int TypeRequest = 0;
            if (model.Title.Contains("UNPAID:"))
            {
                TypeRequest = 1;
            }
            else if(model.Title.Contains("EMERGENCY:"))
            {
                TypeRequest = 2;
            }
            else if(model.Title.Contains("FUNERAL:"))
            {
                TypeRequest = 3;
            }
            else if(model.Title.Contains("PATERNITY:"))
            {
                TypeRequest = 4;
            }

			// Update status of the request
			_emailCancelRequestService.ChangeRequestStatus(model.HeaderRequestId, model.Reason);
			// Send email if success
			// Get request by id
			List<StatusRequestViewModel> data = new List<StatusRequestViewModel>();
			data = _emailCancelRequestService.GetDataRequest(model.HeaderRequestId);
			string employeeEmail = data[0].EmployeeEmail;
			string managerEmail = data[0].ManagerEmail;
			string reasonCancellation = data[0].Reason;
			List<string> emails = new List<string>()
			{
				employeeEmail,
				managerEmail
			};
			_emailCancelRequestService.SendEmails(emails, "Cancel Request", reasonCancellation);
			//Add if 
			if(model.currentStatusId.ToLower() != "rejected")
			{
				_emailCancelRequestService.PlusNoVacDays(employeeEmail, model.NoVacDaysRequested, TypeRequest);
			}
			return RedirectToAction("HistoricalResource");

		}
		[HttpPost]
		public ActionResult ApproveRequest(StatusRequestViewModel model)
		{
		   
			// Update status of the request
			_emailApproveRequestService.ChangeRequestStatus(model.HeaderRequestId, model.Reason);
			// Send email if success
			// Get request by id
			List<StatusRequestViewModel> data = new List<StatusRequestViewModel>();
			data = _emailApproveRequestService.GetDataRequest(model.HeaderRequestId);
			string employeeEmail = data[0].EmployeeEmail;
			string managerEmail = data[0].ManagerEmail;
			string approveReason = data[0].Reason;
			List<string> emails = new List<string>()
			{
				employeeEmail,
				managerEmail
			};
			_emailApproveRequestService.SendEmails(emails, " Approved Request", approveReason);
			//_emailApproveRequestService.LessNoVacDays(employeeEmail, model.NoVacDaysRequested);
			return RedirectToAction("HistoricalResource");
	}
        [HttpPost]
        public ActionResult RejectRequest(StatusRequestViewModel model)
        {
            int TypeRequest = 0;
            if (model.Title.Contains("UNPAID:"))
            {
                TypeRequest = 1;
            }
            else if (model.Title.Contains("EMERGENCY:"))
            {
                TypeRequest = 2;
            }
            else if (model.Title.Contains("FUNERAL:"))
            {
                TypeRequest = 3;
            }
            else if (model.Title.Contains("PATERNITY:"))
            {
                TypeRequest = 4;
            }

            // Update status of the request
            _emailRejectRequestService.ChangeRequestStatus(model.HeaderRequestId, model.Reason);
            // Send email if success
            // Get request by id
            List<StatusRequestViewModel> data = new List<StatusRequestViewModel>();
            data = _emailRejectRequestService.GetDataRequest(model.HeaderRequestId);
            string employeeEmail = data[0].EmployeeEmail;
            string managerEmail = data[0].ManagerEmail;
            string rejectReason = data[0].Reason;
            List<string> emails = new List<string>()
            {
                employeeEmail,
                managerEmail
            };
            _emailRejectRequestService.SendEmails(emails, "Rejected Request", rejectReason);
            _emailCancelRequestService.PlusNoVacDays(employeeEmail, model.NoVacDaysRequested, TypeRequest);

			return RedirectToAction("HistoricalResource");

		}

		[HttpGet]
		public JsonResult ValidateStartDate(DateTime start,  bool flag)
		{
			var sDate = start.AddDays(1);
			DateTime today = DateTime.Today;
			if (sDate.Date > today.Date)
			{
				flag = true;
			}

			//Date confirm
			return Json( flag , JsonRequestBehavior.AllowGet);
		}



		[HttpGet]
		public JsonResult ValidateSameMonth(DateTime start, DateTime end,  bool flag)
		{
			var sDate = start.AddDays(1);
			DateTime today = DateTime.Today;
			if (sDate.Month == end.Month)
			{
				flag = true;
			}

			//Date confirm
			return Json(flag, JsonRequestBehavior.AllowGet);
		}


		[HttpGet]
		public DateTime ValidateResultDate(DateTime returnDate)
		{
			//send parameter where will be validate
			returnDate = returnDate.AddDays(1);
			// check if is holiday 
			var ifIsHoliday = IsHoliday(returnDate);

			// if is Saturday or Sunday
			var ifIsSaturdayOrSunday = IsSatOrSun(ifIsHoliday);

			// if is holiday _Again_
			var ifIsHolidayAgain = IsHoliday(ifIsSaturdayOrSunday);

			//if is final of month
			var isEndOfMonth = IsEndDayOfMonth(ifIsHolidayAgain);

			//if is holiday _Again_
			var ifIsHolidate = IsHoliday(isEndOfMonth);

			// if is Saturday or Sunday\
			var finalDate = IsSatOrSun(ifIsHolidate);


			//Date confirm
			return finalDate;
		}


		public DateTime IsEndDayOfMonth(DateTime returnDate)
		{
		   
			var returnDateVal = returnDate.AddDays(1);
			var finaldate = returnDate;
			if(returnDateVal.Month != returnDate.Month)
			{
				finaldate =  returnDateVal ;
			}
 
			return finaldate;
		}

		public DateTime IsSatOrSun(DateTime returnDate)
		{

			var newDate = new DateTime();
			if (returnDate.DayOfWeek == DayOfWeek.Sunday)
			{
				newDate = returnDate.AddDays(1);
				
			}
			if (returnDate.DayOfWeek == DayOfWeek.Saturday)
			{
				newDate = returnDate.AddDays(2);
			}
			if (returnDate.DayOfWeek != DayOfWeek.Saturday && returnDate.DayOfWeek != DayOfWeek.Sunday)
			{
				newDate = returnDate;
			}

			return newDate;
		}

		public DateTime IsHoliday(DateTime returnDate)
		{
			// Get holidays
			List<Holiday> holidays = _holidayService.GetAllHolidays();

			if (holidays.Count == 0)
			{
				return returnDate;

			}

			else
			{

			
				var newDate = new DateTime();
				foreach (var holiday in holidays)
				{
					if (returnDate.Date == holiday.Day.Date)
					{
						newDate = returnDate.AddDays(1);
						break;
					}
					else
					{
						newDate = returnDate;
					}
				}
				return newDate;

			}


		}

		[HttpGet]
		public JsonResult CountHolidaysAndValidateDates(DateTime start, DateTime end, int count)
		{
			int countH = 0;
			//send parameter where will be validate
			
			 countH = IsHolidayStartAndEndDates(start, end, count);

			//Date confirm
			return Json( countH , JsonRequestBehavior.AllowGet);
		}

		public int IsHolidayStartAndEndDates(DateTime start, DateTime end, int count)
		{
			// Get holidays
			List<Holiday> holidays = _holidayService.GetAllHolidays();
		  
			var sDate = start.AddDays(1);         
			var eDate = end;
			int i = 0;
			DateTime[] date = new DateTime[100];
			while( sDate.Day <= eDate.Day)
			{ 

				date[i] = sDate;
				i++;
				sDate = sDate.AddDays(1);
			}

			var countHolidays = count;

			foreach (var holiday in holidays)
			{
				foreach(var day in date)
				{
					if (day.Date == holiday.Day.Date)
					{
						countHolidays++;
					}
				}
			}
			Array.Clear(date, 0, 100);
			return countHolidays;
			
		}

		//Document this if i dont finish it-----------------------------------------------
		public JsonResult ValidateEmployeeVacationsManager(int IdEmployee)
		{

			Employee SelectedEmployee = new Employee();
			EmployeeService SearchEmployee = new EmployeeService();
			SelectedEmployee = SearchEmployee.GetByID(IdEmployee);

			DateTime FullHireDate = SelectedEmployee.HireDate;
			int vacationDays = SelectedEmployee.Freedays;            
			DateTime today = DateTime.Today;

			int hireYear = FullHireDate.Year;
			int currentYear = today.Year;

			int yearsWorking = currentYear - hireYear;
		
			var flag = false;


			


			if (yearsWorking == 1 && vacationDays > 6)
			{
				flag = true;
			}
			else if (yearsWorking == 2 && vacationDays > 8)
			{
				flag = true;

			}
			else if (yearsWorking == 3 && vacationDays > 10)
			{
				flag = true;

			}
			else if (yearsWorking == 4 && vacationDays > 12)
			{
				flag = true;

			}
			else if ( (yearsWorking >= 5 && yearsWorking <= 9) && vacationDays > 12) 
			{
				flag = true;

			}
			else
			{
				flag = false;
			}


			return Json(flag, JsonRequestBehavior.AllowGet);
		}
		//Document this if i dont finish it-----------------------------------------------


		public JsonResult ValidationStarEndDatesHolidays (DateTime startDate, DateTime endDate, bool unpaid = false)
		{
			//var sDate = startDate;
			//var eDate = endDate;
			//these are the constans that will be compare with the days
			DateTime DiaStarRango = startDate.Date.AddDays(1);
			//DateTime SabadoFecha = new DateTime(2018, 4, 28);
			//DateTime DomingoFecha = new DateTime(2018, 4, 29);

			int DiasARestarSabadosDomingos = 0;
			//this while will be counting the endweeks days
			while (true)
			{
				if(DiaStarRango.Date == endDate.Date)
				{

					//var SabadoComparacion = SabadoFecha.DayOfWeek;
					//var DomingoComparacion = DomingoFecha.DayOfWeek;
					var DiaSemana = DiaStarRango.DayOfWeek;

					if (DiaSemana == DayOfWeek.Saturday || DiaSemana == DayOfWeek.Sunday)
					{
						DiasARestarSabadosDomingos = DiasARestarSabadosDomingos + 1;
					}

					DiaStarRango = DiaStarRango.AddDays(1);

					break;
				}
				else if(endDate < DiaStarRango.Date)
				{
					break;
				}
				else
				{
					//var SabadoComparacion = SabadoFecha.DayOfWeek;
					//var DomingoComparacion = DomingoFecha.DayOfWeek;
					var DiaSemana = DiaStarRango.DayOfWeek;

					if(DiaSemana == DayOfWeek.Saturday || DiaSemana == DayOfWeek.Sunday)
					{
						DiasARestarSabadosDomingos = DiasARestarSabadosDomingos + 1;
					}

					DiaStarRango = DiaStarRango.AddDays(1);
				}
			}

			//here we are resting the endweek days to work days
			int countMethod = 0;

			int CountingHolidays = IsHolidayStartAndEndDates(startDate, endDate, countMethod);

			TimeSpan DiasEntreFechas = endDate - startDate;

			int dias = DiasEntreFechas.Days - DiasARestarSabadosDomingos;
			int finalDays = dias - CountingHolidays;
			DateTime FecharCortaReturnDate = ValidateResultDate(endDate);
			string returnDateCorrectly = FecharCortaReturnDate.ToString("MM/dd/yyyy");
			DateTime returnDate = DateTime.Now;

			var dataToReturn = new
			{
				IsValid = default(bool),
				Message = default(string),
				errorType = default(int),
				NumberDaysRequested = default(int),
				ReturnDate = default(string)
			};

			if (endDate.Date > startDate.Date)
			{
				//all is correct
			}

			else
			{
				dataToReturn = new
				{
					IsValid = false,
					Message = "End date should be higest than Start Date",
					errorType = 3,
					NumberDaysRequested = finalDays,
					ReturnDate = returnDateCorrectly
				};

				return Json(dataToReturn, JsonRequestBehavior.AllowGet);
			}

			//Validated if start date is valid
			#region validation start date
			var validateStartDate_sDate = startDate.AddDays(1);
			DateTime today = DateTime.Today;
			if(validateStartDate_sDate.Date > today.Date)
			{
				//all is correct
			}
			else
			{
				dataToReturn = new
				{
					IsValid = false,
					Message = "Start date is wrong, please checkt it and correct",
					errorType = 1,
					NumberDaysRequested = finalDays,
					ReturnDate = returnDateCorrectly
				};

				return Json(dataToReturn, JsonRequestBehavior.AllowGet);
			}
			#endregion

			//validate if startdate star after 5 weeks
			var weekNumberfive = today.AddDays(35);
			if(validateStartDate_sDate.Date > weekNumberfive.Date)
			{
				//all is correct
			}
			else
			{
				dataToReturn = new
				{
					IsValid = false,
					Message = "Start date sould be after 5 weeks",
					errorType = 2,
					NumberDaysRequested = finalDays,
					ReturnDate = returnDateCorrectly
				};

				return Json(dataToReturn, JsonRequestBehavior.AllowGet);
			}

			//This will be the correct data
			dataToReturn = new
			{
				IsValid = true,
				Message = "All is correct",
				errorType = 0,
				NumberDaysRequested = finalDays,
				ReturnDate = returnDateCorrectly
			};

			return Json(dataToReturn, JsonRequestBehavior.AllowGet);
		}
	}
}

