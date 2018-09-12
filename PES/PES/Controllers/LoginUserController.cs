using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PES.Models;
using PES.Services;
using System.Web.Security;

namespace PES.Controllers
{
    [AllowAnonymous]
    public class LoginUserController : Controller
    {
        public string UserEmail;
        EmployeeService _employeeService;

        public LoginUserController()
        {
            _employeeService = new EmployeeService();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        // [Authorize(Order=1 ,Roles="UserEmail", Users="Employee")]
        public ActionResult Login(Login user)
        {
            if (_employeeService.GetByEmail(user.UserEmail).EndDate == null)
            {
                //var isAuthenticated = user.Authentication(user.UserEmail, user.Password);
                var isAuthenticated = true; //Added for testing when offline

                //Check if the Email and Password exist in Office 356
                if (isAuthenticated)
                {
                    // Get user 
                    Employee resource = _employeeService.GetByEmail(user.UserEmail);
                    Login authentication;
                    // Validate if resource not found
                    if (resource != null)
                    {
                        // Store the Resource profile in a variable session
                        Session["UserProfile"] = (int)resource.ProfileId;
                        // Store the Resource user name in a variable session
                        Session["UserEmail"] = resource.Email;
                        // Set flag that user is logged in
                        Session["UserName"] = string.Format("{0} {1}", resource.FirstName, resource.LastName);
                        // Store the Resource user id in a variable session
                        Session["UserId"] = resource.EmployeeId;
                        // Set user as authenticated
                        FormsAuthentication.SetAuthCookie(user.UserEmail, true);
                        //Decide if the user is a Resouce
                        if ((ProfileUser)resource.ProfileId == ProfileUser.Resource)
                        {
                            //Return the Resource's view 
                            //return RedirectToAction("ChoosePeriod", "PerformanceEvaluation", new { employeeEmail = resource.Email, employeeID = resource.EmployeeId });

                            return RedirectToAction("Index", "Home");
                        }
                        //Check if the user is a Manager or Director 
                        else if ((ProfileUser)resource.ProfileId== ProfileUser.Manager)
                        {
                            //Return the Manager's view                   
                            //return RedirectToAction("Index", "PerformanceEvaluation");
                            return RedirectToAction("Index", "Home");
                        }
                        else if ((ProfileUser)resource.ProfileId ==  ProfileUser.Director)
                        {
                            //Return the Manager's view
                            //return RedirectToAction("Index", "PerformanceEvaluation");
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            // Return to the login screen if no profile
                            // Message: You are not allowed
                            TempData["Error"] = "Resource not found";
                            return RedirectToAction("Login", "LoginUser");
                        }
                    }
                    else
                    {
                        // resource not found
                        TempData["Error"] = "Resource not found";
                        return View("Login", user);
                    }
                }
                else
                {
                    // Return the login view if the user isn't valid 
                    // Message: User or password are not valid
                    TempData["Error"] = "User or password not correct. Please try again.";
                    return View("Login", user);
                }
            }
            else
            {
                // Return the login view if the user isn't enabled 
                // Message: User disabled to log in
                TempData["Error"] = "User disabled to login";
                return View("Login", user);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "LoginUser");
        }
    }
}