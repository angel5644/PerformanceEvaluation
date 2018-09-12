using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PES.Models
{
    public static class ProfilesOwners
    {
        public static string Director 
        { 
            get { return "miguel.hernandez@4thsource.com"; } 
        }

        public static List<string> Managers 
        {
            get 
            {
                return new List<string>()
                {
                    "Carlos.esparza@4thsource.com",
                    "jose.gutierrez@4thsource.com",
                    "eder.palacios@4thsource.com",
                    "jose.salazar@4thsource.com"
                };
            }
        }

        public static bool AddManager() 
        {
            return false;
        }
    }
}