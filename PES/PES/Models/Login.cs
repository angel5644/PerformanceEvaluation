using Microsoft.Exchange.WebServices.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public class Login
    {
        //Get UserName and Password 
        [Required(ErrorMessage = "The user name (email) is required")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
        

        //Authentication Office 365
        public bool Authentication(string username, string pass)
        {
            try
            {
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013);
                service.Credentials = new WebCredentials(username, pass, null);
                service.Url = new Uri("https://outlook.office365.com/ews/exchange.asmx");
                ItemView view = new ItemView(1);
                if (service.FindItems(WellKnownFolderName.Inbox, view).TotalCount >= 0)
                {

                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }


        public bool AuthorizeCore(HttpContextBase httpContext, int profile)
        {
            var user = httpContext.User;
            if (profile == (int)ProfileUser.Manager)
            {
                user.IsInRole("Manager");
            }
            else if (profile == (int)ProfileUser.Director)
            {
                user.IsInRole("Director");
            }
            return true;
        }
    }     
}