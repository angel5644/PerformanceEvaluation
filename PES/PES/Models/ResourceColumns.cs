using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PES.Models
{
    /// <summary>
    /// Column names of Resource Excel file
    /// Note: Make sure to change this class if column names in Resource Excel file changes
    /// </summary>
    public static class ResourceColumns
    {
        public static string Name
        {
            get { return "Name"; }
        }
        public static string Email
        {
            get { return "Email"; }
        }
        public static string Manager
        {
            get { return "Manager"; }
        }
        public static string UserID
        {
            get { return "User ID"; }
        }
        public static string LastName
        {
            get { return "Last name"; }
        }
        public static string FirstName
        {
            get { return "First name"; }
        }
        public static string JobCode
        {
            get { return "Job code"; }
        }
        public static string Active
        {
            get { return "Active"; }
        }

        public static List<string> GetColumnList()
        {
            List<string> columnList = new List<string>();
            Type classType = typeof(ResourceColumns);
            FieldInfo[] fields = classType.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                columnList.Add(field.GetValue(null).ToString());
            }

            return columnList;
        }
    }
}