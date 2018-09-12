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

namespace PES.Controllers
{
    [AllowAnonymous]
    public class EmployeeController : Controller
    {

        /// <summary>
        /// /esto es el cambio
        /// mas cambios 
        /// mas canmbios 
        /// mas cambios 
        /// </summary>
        // Declare services here
        private ProfileService _profileService;
        private EmployeeService _employeeService;
        private LocationService _locationService;

        public EmployeeController()
        {
            _profileService = new ProfileService();
            _employeeService = new EmployeeService();
            _locationService = new LocationService();
        }

        // GET: Employee
        public ActionResult Index()
        {

            Login mail = new Login();
            EmployeeService getEmployee = new EmployeeService();
            Employee Employee = new Employee();

            Employee = getEmployee.GetByEmail(mail.UserEmail);
            return View(Employee);
        }

        [HttpGet]
        public ActionResult InsertEmployee()
        {
            InsertEmployeeViewModel model = new InsertEmployeeViewModel();

            // Get profile current user 
            var profileUser = Session["UserProfile"];

            if (profileUser == null)
            {
                // user not logged in, return home
                TempData["Error"] = "You need to log in first.";
                return RedirectToAction("Login", "LoginUser");
            }

            // Get current user  
            Employee currentUser = new Employee();
            EmployeeService EmployeeService = new EmployeeService();
            currentUser = EmployeeService.GetByEmail((string)Session["UserEmail"]);

            model = SetUpDropdowns(model);

            if (currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                // user is resource not allowed, return to home  
                TempData["Error"] = "You're resource. You're not allowed to insert employees.";
                return RedirectToAction("Index", "PerformanceEvaluation");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult InsertEmployee(InsertEmployeeViewModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = new Employee();

                newEmployee.FirstName = employeeModel.FirstName;
                newEmployee.LastName = employeeModel.LastName;
                newEmployee.Email = employeeModel.Email + "@4thsource.com";
                newEmployee.ProfileId = employeeModel.SelectedProfile;
                newEmployee.ManagerId = employeeModel.SelectedManager;
                newEmployee.Customer = "No customer";
                newEmployee.Position = "Not specified";
                newEmployee.LocationId = employeeModel.SelectedLocation;

                if (uniqueEmail(newEmployee))
                {
                    _employeeService.InsertEmployee(newEmployee);
                    
                    // Success message
                    TempData["Success"] = "Successfully registered";
                    var newDirectorInserted = _employeeService.GetByEmail(newEmployee.Email);
                    if (newDirectorInserted.ProfileId == (int)ProfileUser.Director)
                    {
                        _employeeService.InsertDirector(newDirectorInserted);
                        return RedirectToAction("ViewEmployees");
                    }
                    else
                    {
                        return RedirectToAction("ViewEmployees");
                    }

                    
                }
                else
                {
                    // Error message
                    TempData["Error"] = "This email is already registered";

                    employeeModel = SetUpDropdowns(employeeModel);
                    return View(employeeModel);
                }
            }

            employeeModel = SetUpDropdowns(employeeModel);
            return View(employeeModel);
        }

        #region verify uniqueEmail
        private bool uniqueEmail(Employee employee)
        {
            var counter = 0;
            List<Employee> EmployeeList = new List<Employee>();
            EmployeeList = _employeeService.GetAll();

            foreach (var item in EmployeeList)
            {
                if (employee.Email == item.Email)
                {
                    counter++;
                    if (counter > 0)
                        break;
                }
            }

            if (counter == 0)
                return true;
            else
                return false;
        }
        #endregion

        #region setting up dropdowns

        private InsertEmployeeViewModel SetUpDropdowns(InsertEmployeeViewModel model)
        {
            // Get current user 
            //var profileUser = Session["UserProfile"];
            var currenUser = _employeeService.GetByEmail(Session["UserEmail"].ToString());
            // Get profiles
            var profiles = _profileService.GetAllProfiles();

            // Populate profiles 
            List<SelectListItem> profilesList = new List<SelectListItem>();
            foreach (var profile in profiles)
            {
                var newItem = new SelectListItem()
                {
                    Text = profile.Name,
                    Value = (profile.ProfileId).ToString(),
                    Selected = false
                };
                if (currenUser.ProfileId == (int)ProfileUser.Director)
                {
                    profilesList.Add(newItem);
                }
                else if (currenUser.ProfileId == (int)ProfileUser.Manager && newItem.Text == "Resource")
                {
                    profilesList.Add(newItem);
                }

            }

            // Get managers 
            List<Employee> managers = _employeeService.GetAll();

            //Populate managers
            List<SelectListItem> managersList = new List<SelectListItem>();
            foreach (var manager in managers)
            {
                if (currenUser.ProfileId == (int)ProfileUser.Director && manager.ProfileId == (int)ProfileUser.Director || manager.ProfileId == (int)ProfileUser.Manager)
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
                else if (currenUser.ProfileId == (int)ProfileUser.Manager && manager.ProfileId == (int)ProfileUser.Manager)
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
            }

            //Get locations
            var locations = _locationService.GetAll();
            //Get current location

            //populate locations
            List<SelectListItem> locationsList = new List<SelectListItem>();
            foreach(var item in locations)
            {
                var location = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.LocationId.ToString(),
                    Selected = false
                };
                locationsList.Add(location);
            }
            //Setting default managers and profiles
            profilesList = setDefaultProfile(profilesList, currenUser);
            managersList = setDefaultManager(managersList, currenUser);

            #region Set data
            // Set profiles
            model.ListProfiles = profilesList;
            model.ListManagers = managersList;
            model.ListLocations = locationsList;

            #endregion

            return model;
        }

        private List<SelectListItem> setDefaultProfile(List<SelectListItem> listProfiles, Employee employee)
        {
            var defaultProfile = listProfiles.Find(x => x.Value == (employee.ProfileId - 1).ToString());
            if(defaultProfile != null)
            {
                defaultProfile.Selected = true;
            }
            return listProfiles;
        }
        private List<SelectListItem> setDefaultManager(List<SelectListItem> listManagers, Employee employee)
        {
            var defaultManager = listManagers.Find(x => x.Value == employee.EmployeeId.ToString());
            if(defaultManager != null)
            {
                defaultManager.Selected = true;
            }
            return listManagers;
        }

        private UpdateEmployeeViewModel SetUpDropdowns(UpdateEmployeeViewModel model)
        {
            // Get profile current user 
            var profileUser = Session["UserProfile"];
            // Get profiles
            var profiles = _profileService.GetAllProfiles();

            // Populate profiles 
            List<SelectListItem> profilesList = new List<SelectListItem>();
            foreach (var profile in profiles)
            {
                var newItem = new SelectListItem()
                {
                    Text = profile.Name,
                    Value = (profile.ProfileId).ToString(),
                    Selected = false
                };
                if ((int)profileUser == (int)ProfileUser.Director)
                {
                    profilesList.Add(newItem);
                }
                else if ((int)profileUser == (int)ProfileUser.Manager && newItem.Text == "Resource")
                {
                    profilesList.Add(newItem);
                }
            }

            // Get managers 
            List<Employee> managers = _employeeService.GetAll();

            //Populate managers
            List<SelectListItem> managersList = new List<SelectListItem>();
            foreach (var manager in managers)
            {
                if ((int)profileUser == (int)ProfileUser.Director && (manager.ProfileId == Convert.ToInt32(ProfileUser.Manager) || manager.ProfileId == Convert.ToInt32(ProfileUser.Director)))
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
                else if ((int)profileUser == (int)ProfileUser.Manager && manager.ProfileId == Convert.ToInt32(ProfileUser.Manager))
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
            }
            //Get locations
            var locations = _locationService.GetAll();
            //Get current location

            //populate locations
            List<SelectListItem> locationsList = new List<SelectListItem>();
            foreach (var item in locations)
            {
                var location = new SelectListItem
                {
                    Text = item.Name,
                    Value = item.LocationId.ToString(),
                    Selected = false
                };
                locationsList.Add(location);
            }

            #region Set data
            // Set profiles
            model.ListProfiles = profilesList;
            model.ListManagers = managersList;
            model.ListLocation = locationsList;

            #endregion

            return model;
        }

        private TransferEmployeeViewModel SetUpDropdowns(TransferEmployeeViewModel model)
        {
            // Get profile current user 
            var profileUser = Session["UserProfile"];
            // Get profiles
            var profiles = _profileService.GetAllProfiles();

            // Populate profiles 
            List<SelectListItem> profilesList = new List<SelectListItem>();
            foreach (var profile in profiles)
            {
                var newItem = new SelectListItem()
                {
                    Text = profile.Name,
                    Value = (profile.ProfileId).ToString(),
                    Selected = false
                };
                if ((int)profileUser == (int)ProfileUser.Director && (newItem.Text == "Director" || newItem.Text == "Manager"))
                {
                    profilesList.Add(newItem);
                }
                else if ((int)profileUser == (int)ProfileUser.Manager && newItem.Text == "Manager")
                {
                    profilesList.Add(newItem);
                }
            }

            // Get managers 
            List<Employee> managers = _employeeService.GetAll();

            //Populate managers
            List<SelectListItem> managersList = new List<SelectListItem>();
            foreach (var manager in managers)
            {
                if ((int)profileUser == (int)ProfileUser.Director && (manager.ProfileId == Convert.ToInt32(ProfileUser.Manager) || manager.ProfileId == Convert.ToInt32(ProfileUser.Director)))
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
                else if ((int)profileUser == (int)ProfileUser.Manager && manager.ProfileId == Convert.ToInt32(ProfileUser.Manager))
                {
                    var newItem = new SelectListItem()
                    {
                        Text = manager.FirstName + " " + manager.LastName,
                        Value = (manager.EmployeeId).ToString(),
                        Selected = false
                    };
                    managersList.Add(newItem);
                }
            }

            #region Set data
            // Set profiles
            model.ProfilesList = profilesList;
            model.ManagerAList = managersList;
            model.ManagerBList = managersList;

            #endregion

            return model;
        }
        #endregion

        public JsonResult GetEmployeesProifile(int profile, string email)
        {
            //Get curren employee selected
            var currentEmployee = _employeeService.GetByEmail(email + "@4thsource.com");
            if (currentEmployee.EmployeeId == Convert.ToInt32(ProfileUser.None))
            {
                currentEmployee = _employeeService.GetByEmail(email);
            }
            //Get all employees depending profile
            var employees = _employeeService.GetByPorfileId((profile));
            var item = employees.Find(x => x.EmployeeId == currentEmployee.EmployeeId);
            employees.Remove(item);
            var has = _employeeService.GetEmployeeByManager(currentEmployee.EmployeeId).Count;

            //Return employees json file
            return Json(new { employees = employees, hasOrg = has }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetEmployeesByProifile(int profile)
        {
            //Get all employees depending profile
            var employees = _employeeService.GetByPorfileId(profile);

            //Return employees json file
            return Json(new { employees = employees}, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            UpdateEmployeeViewModel model = new UpdateEmployeeViewModel();

            // Get profile current user 
            var profileUser = Session["UserProfile"];

            if (profileUser == null)
            {
                // user not logged in, return home
                TempData["Error"] = "You need to log in first.";
                return RedirectToAction("Login", "LoginUser");
            }

            // Get current user  
            Employee currentUser = new Employee();
            EmployeeService EmployeeService = new EmployeeService();
            currentUser = EmployeeService.GetByEmail((string)Session["UserEmail"]);

            model = SetUpDropdowns(model);

            if (currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                // user is resource not allowed, return to home  
                TempData["Error"] = "You're resource. You're not allowed to update employees.";
                return RedirectToAction("Index", "PerformanceEvaluation");
            }
            else
            {
                var employee = _employeeService.GetByID(id);

                model.EmployeeId = employee.EmployeeId;
                model.FirstName = employee.FirstName;
                model.LastName = employee.LastName;

                string email = employee.Email;
                var emailUsername = email.Split('@');

                model.Email = emailUsername[0];
                model.SelectedProfile = employee.ProfileId;
                model.SelectedManager = employee.ManagerId;
                model.SelectedLocation = employee.LocationId;

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult UpdateEmployee(UpdateEmployeeViewModel employeeModel)
        {
            // Get current user  
            Employee currentUser = new Employee();
            EmployeeService employeeService = new EmployeeService();
            currentUser = employeeService.GetByEmail((string)Session["UserEmail"]);

            if (ModelState.IsValid)
            {
                var newEmployee = new Employee();

                newEmployee.EmployeeId = employeeModel.EmployeeId;
                newEmployee.FirstName = employeeModel.FirstName;
                newEmployee.LastName = employeeModel.LastName;
                newEmployee.Email = employeeModel.Email + "@4thsource.com";
                newEmployee.ProfileId = employeeModel.SelectedProfile;
                if(employeeModel.SelectedProfile == (int)ProfileUser.Director)
                {
                    newEmployee.ManagerId = employeeModel.EmployeeId;
                }
                else
                {
                    newEmployee.ManagerId = employeeModel.SelectedManager;
                }
                newEmployee.LocationId = employeeModel.SelectedLocation;

                _employeeService.UpdateEmployee(newEmployee);

                // Success message
                TempData["Success"] = "Successfully updated";
                return RedirectToAction("ViewEmployees", "Employee");
            }
            employeeModel = SetUpDropdowns(employeeModel);
            return View(employeeModel);
        }

        public ActionResult ViewEmployees()
        {
            // Get current users by using email in Session
            // Get current user 
            Employee currentUser = new Employee();
            var userEmail = (string)Session["UserEmail"];
            var location = new Location();

            currentUser = _employeeService.GetByEmail(userEmail);

            if (currentUser.ProfileId == (int)ProfileUser.Resource)
            {
                //Resource is only allowed to view his own info
                return RedirectToAction("EmployeeDetails/" + currentUser.EmployeeId);
            }
            else if (currentUser.ProfileId == (int)ProfileUser.Manager)
            {
                //Get employees of manager org
                List<Employee> EmployeeList = _employeeService.GetEmployeeByManager(currentUser.EmployeeId);
                List<EmployeeDetailsViewModel> ModelList = new List<EmployeeDetailsViewModel>();

                foreach (var employee in EmployeeList)
                {
                    EmployeeDetailsViewModel model = new EmployeeDetailsViewModel();

                    //get porfiles
                    var porfile = _profileService.GetProfileByID(employee.ProfileId);
                    var manager = _employeeService.GetByID(employee.ManagerId);
                    location = _locationService.GetPeriodById(employee.LocationId);

                    model.EmployeeId = employee.EmployeeId;
                    model.FirstName = employee.FirstName;
                    model.LastName = employee.LastName;
                    model.Email = employee.Email;
                    model.Customer = employee.Customer;
                    model.Position = employee.Position;
                    model.ProfileId = employee.ProfileId;
                    model.ManagerId = employee.ManagerId;
                    model.EndDate = employee.EndDate;
                    model.Project = employee.Project;
                    model.Profile = porfile;
                    model.Manager = manager;
                    model.Location = location;

                    if (model.EmployeeId != currentUser.EmployeeId)
                    {
                        ModelList.Add(model);
                    }
                }

                return View(ModelList);
            }
            else if (currentUser.ProfileId == (int)ProfileUser.Director)
            {
                // Get all employees
                List<Employee> employeeList = _employeeService.GetEmployeesByDirector(currentUser.EmployeeId);
                List<EmployeeDetailsViewModel> modelList = new List<EmployeeDetailsViewModel>();

                foreach (var employee in employeeList)
                {
                    EmployeeDetailsViewModel model = new EmployeeDetailsViewModel();

                    //get porfiles
                    var porfile = _profileService.GetProfileByID(employee.ProfileId);
                    var manager = _employeeService.GetByID(employee.ManagerId);
                    location = _locationService.GetPeriodById(employee.LocationId);

                    model.EmployeeId = employee.EmployeeId;
                    model.FirstName = employee.FirstName;
                    model.LastName = employee.LastName;
                    model.Email = employee.Email;
                    model.Customer = employee.Customer;
                    model.Position = employee.Position;
                    model.ProfileId = employee.ProfileId;
                    model.ManagerId = employee.ManagerId;
                    model.EndDate = employee.EndDate;
                    model.Project = employee.Project;
                    model.Profile = porfile;
                    model.Manager = manager;
                    model.Location = location;

                    if (model.EmployeeId != currentUser.EmployeeId)
                    {
                        modelList.Add(model);
                    }
                }

                return View(modelList);
            }
            else
            {
                // Error if not logged in
                TempData["Error"] = "You are not logged in.";

                return RedirectToAction("Login", "UserLogin");
            }

        }

        public async Task<ActionResult> GetEmployeesByManager(int employeeId, int option)
        {
            TransferEmployeeViewModel model = new TransferEmployeeViewModel();     
            
            // Get user
            var user = _employeeService.GetByID(employeeId);
            // Get profile of user
            var profile = _profileService.GetProfileByID(user.ProfileId);
            // Get employees of the user, depending on its profile
            var employees = _employeeService.GetEmployeeByManager(employeeId);

            List<Employee> filteredEmployees = new List<Employee>();

            filteredEmployees = employees.Where(e => e.EmployeeId != user.EmployeeId).ToList();
            model.ManagerEmployeeList = filteredEmployees;

            SetUpDropdowns(model);

            if (option == 1)               
                return PartialView("_TransferEmployeePartial", model);
            else
                return PartialView("_TransferEmployeePartial2", model);         
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployeesByFilter(string employeeEmail, string filter)
        {
            List<Employee> employeesFiltered = new List<Employee>();
            // Get user 
            var user = _employeeService.GetByEmail(employeeEmail);

            // Get profile of user
            var profile = _profileService.GetProfileByID(user.ProfileId);

            // Get employees of the user, depending on its profile
            var employees = _employeeService.GetEmployeesByProfile(user.EmployeeId, profile.ProfileId);
            
            // Validate filter
            if (filter == "enabled")
            {
                employeesFiltered = employees.Where(e => e.EndDate == null).ToList();
            }
            else if (filter == "disabled")
            {
                employeesFiltered = employees.Where(e => e.EndDate != null).ToList();                
            }
            else
            {
                employeesFiltered = employees;
            }

            // Create model (partial view)            
            List<EmployeeDetailsViewModel> modelList = new List<EmployeeDetailsViewModel>();
            var location = new Location();

            // Populate model with employees data
            foreach (var item in employeesFiltered)
            {
                EmployeeDetailsViewModel model = new EmployeeDetailsViewModel();

                //get porfiles
                var porfile = _profileService.GetProfileByID(item.ProfileId);
                var manager = _employeeService.GetByID(item.ManagerId);
                location = _locationService.GetPeriodById(item.LocationId);

                model.EmployeeId = item.EmployeeId;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.Email = item.Email;
                model.Profile = porfile;
                model.Manager = manager;
                model.ProfileId = item.ProfileId;
                model.ManagerId = item.ManagerId;
                model.Director = _employeeService.GetByID(model.Manager.ManagerId);
                model.EndDate = item.EndDate;
                model.Location = location;

                if(model.EmployeeId != model.Manager.EmployeeId)
                {
                    modelList.Add(model);
                }
            }

            return PartialView("_ViewEmployeesPartial", modelList);
        }
        
        public void DisableEmployeeInList(int id)
        {
            var employee = _employeeService.GetByID(id);
            employee.EndDate = DateTime.Now;
            _employeeService.UpdateEmployee(employee);
        }

        [HttpGet]
        public ActionResult EmployeeDetails(string email)
        {
            var employee = _employeeService.GetByEmail(email);

            // Get manager
            var manager = _employeeService.GetByID(employee.ManagerId);
            //Get director
            var director = _employeeService.GetByID(manager.ManagerId);
            // Get profile
            var porfile = _profileService.GetProfileByID(employee.ProfileId);

            var location = _locationService.GetPeriodById(employee.LocationId);

            EmployeeDetailsViewModel model = new EmployeeDetailsViewModel();
            model.EmployeeId = employee.EmployeeId;
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Email = employee.Email;
            model.ProfileId = employee.ProfileId;
            model.ManagerId = employee.ManagerId;
            model.EndDate = employee.EndDate;
            model.Profile = porfile;
            model.Manager = manager;
            model.Director = director;
            model.Location = location;

            return View(model);
        }

        public JsonResult EnableDisableEmployee(int idEmployee, string option)
        {
            var disabledEmployee = _employeeService.GetByID(idEmployee);
            if (option == "enable")
            {
                disabledEmployee.EndDate = null;
            }
            else
            {
                disabledEmployee.EndDate = DateTime.Now;
            }
            _employeeService.UpdateEmployee(disabledEmployee);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ChangeProfile(string email)
        {
            ChangeProfileViewModel changedEmployee = new ChangeProfileViewModel();
            var employee = _employeeService.GetByEmail(email + "@4thsource.com");

            if ((int)Session["UserProfile"] != (int)ProfileUser.Resource)
            {
                changedEmployee.FirstName = employee.FirstName;
                changedEmployee.LastName = employee.LastName;
                changedEmployee.Email = employee.Email;
                changedEmployee.SelectedProfile = employee.ProfileId;
                changedEmployee.SelectedManager = employee.ManagerId;
                changedEmployee.CurrentProfile = _profileService.GetProfileByID(employee.ProfileId);
                SetUpDropdowns(changedEmployee);

                foreach (var item in _employeeService.GetEmployeeByManager(employee.EmployeeId)) {
                    changedEmployee.Assigned++;
                }

                return View(changedEmployee);
            }
            else if ((int)Session["UserProfile"] == (int)ProfileUser.Resource)
            {
                TempData["Error"] = "Resources are not allowed to change his profile";
                return RedirectToAction("ViewEmployees");
            }
            else
            {
                TempData["Error"] = "You are not logged in.";
                return RedirectToAction("Login", "LoginUser");
            }

        }

        [HttpPost]
        public ActionResult ChangeProfile(ChangeProfileViewModel model)
        {
            var changedEmployee = _employeeService.GetByEmail(model.Email + "@4thsource.com");
            if(model.SelectedProfile == (int)ProfileUser.Director)
            {
                changedEmployee.ManagerId = changedEmployee.EmployeeId;
            }
            else
            {
                changedEmployee.ManagerId = model.SelectedManager;
            }
            changedEmployee.ProfileId = model.SelectedProfile;

            //Send info to service
            if (_employeeService.TransferAllEmployees(changedEmployee.EmployeeId, model.NewManager))
            {
                TempData["Success"] = "Employees in your org have been transferred successfully.";
                _employeeService.UpdateEmployee(changedEmployee);
                TempData["Success"] = "The profile has been successfully updated.";

                if (changedEmployee.Email == Session["UserEmail"].ToString())
                    return RedirectToAction("Logout", "LoginUser");
                else
                    return RedirectToAction("ViewEmployees");
            }
            else
            {
                TempData["Error"] = "Employees transfering error. Please verify your information.";
                return View(model);
            }

        }
        
        [HttpGet]
        public ActionResult TransferEmployees()
        {
            TransferEmployeeViewModel transferModel = new TransferEmployeeViewModel();
            //Get current user
            var currentUser = _employeeService.GetByEmail(Session["UserEmail"].ToString());

            if (currentUser.ProfileId == (int)ProfileUser.Director)
            {
                List<Employee> employeeList = new List<Employee>();
                transferModel.ManagerEmployeeList = employeeList;
                SetUpDropdowns(transferModel);
                
                return View(transferModel);
            }
            else
            {
                TempData["Error"] = "You are not allowed to tranfer employees.";
                return RedirectToAction("ViewEmployees");
            }
        }

        [HttpPost]
        public JsonResult TransferEmployee(int[] employeesId, int manager)
        {
            try
            {
                for (int i = 0; i < employeesId.Length; i++)
                {
                    _employeeService.TransferEmployees(employeesId[i], manager);
                }

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}