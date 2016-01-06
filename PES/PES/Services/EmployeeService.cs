﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;

namespace PES.Services
{
    public class EmployeeService
    {
        private PESDBContext dbContext;
        public string Profile, Name;

        /// <summary>
        /// Column where the count of the number of rows will be gotten
        /// </summary>
        public const string countColumn = "Email";

        public EmployeeService()
        {
            dbContext = new PESDBContext();
        }

        //Get ONE employee By Email
        public Employee GetByEmail(string email)
        {
            Employee employee = new Employee();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_EMPLOYEE," +
                                           "FIRST_NAME," +
                                           "LAST_NAME," +
                                           "EMAIL," +
                                           "CUSTOMER," +
                                           "POSITION," +
                                           "ID_PROFILE," +
                                           "ID_MANAGER," +
                                           "HIRE_DATE," +
                                           "RANKING," +
                                           "END_DATE " +
                                           "FROM EMPLOYEE WHERE EMAIL = '" + email + "'";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        employee = new Employee();
                        employee.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                        employee.FirstName = Convert.ToString(Read["FIRST_NAME"]);
                        employee.LastName = Convert.ToString(Read["LAST_NAME"]);
                        employee.Email = Convert.ToString(Read["EMAIL"]);
                        employee.Customer = Convert.ToString(Read["CUSTOMER"]);
                        employee.Position = Convert.ToString(Read["POSITION"]);
                        employee.ProfileId = Convert.ToInt32(Read["ID_PROFILE"]);
                        employee.ManagerId = Convert.ToInt32(Read["ID_MANAGER"]);
                        employee.HireDate = Convert.ToDateTime(Read["HIRE_DATE"]);
                        employee.Ranking = Convert.ToInt32(Read["RANKING"]);
                        string endDate = Convert.ToString(Read["END_DATE"]);

                        if (!string.IsNullOrEmpty(endDate))
                        {
                            employee.EndDate = Convert.ToDateTime(endDate);
                        }
                        else
                        {
                            employee.EndDate = null;
                        }
                    }

                    db.Close();
                }
            }
            catch
            {
                employee = null;
            }
            return employee;
        }

        //Get ONE employee by ID
        public Employee GetByID(int ID)
        {
            Employee employee = new Employee();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_EMPLOYEE," +
                                           "FIRST_NAME," +
                                           "LAST_NAME," +
                                           "EMAIL," +
                                           "CUSTOMER," +
                                           "POSITION," +
                                           "ID_PROFILE," +
                                           "ID_MANAGER," +
                                           "HIRE_DATE," +
                                           "RANKING," +
                                           "END_DATE " +
                                           "FROM EMPLOYEE WHERE ID_EMPLOYEE = '" + ID + "'";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        employee = new Employee();
                        employee.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                        employee.FirstName = Convert.ToString(Read["FIRST_NAME"]);
                        employee.LastName = Convert.ToString(Read["LAST_NAME"]);
                        employee.Email = Convert.ToString(Read["EMAIL"]);
                        employee.Customer = Convert.ToString(Read["CUSTOMER"]);
                        employee.Position = Convert.ToString(Read["POSITION"]);
                        employee.ProfileId = Convert.ToInt32(Read["ID_PROFILE"]);
                        employee.ManagerId = Convert.ToInt32(Read["ID_MANAGER"]);
                        employee.HireDate = Convert.ToDateTime(Read["HIRE_DATE"]);
                        employee.Ranking = Convert.ToInt32(Read["RANKING"]);
                        string endDate = Convert.ToString(Read["END_DATE"]);

                        if (!string.IsNullOrEmpty(endDate))
                        {
                            employee.EndDate = Convert.ToDateTime(endDate);
                        }
                        else
                        {
                            employee.EndDate = null;
                        }
                    }

                    db.Close();
                }
            }
            catch
            {
                employee = null;
            }
            return employee;
        }

        //Get all employees
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT ID_EMPLOYEE, " +
                                           "FIRST_NAME, " +
                                           "LAST_NAME, " +
                                           "EMAIL, " +
                                           "CUSTOMER, " +
                                           "POSITION, " +
                                           "ID_PROFILE," +
                                           "ID_MANAGER, " +
                                           "HIRE_DATE, " +
                                           "RANKING, " +
                                           "END_DATE " +
                                           "FROM EMPLOYEE";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        employee = new Employee();
                        employee.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                        employee.FirstName = Convert.ToString(Read["FIRST_NAME"]);
                        employee.LastName = Convert.ToString(Read["LAST_NAME"]);
                        employee.Email = Convert.ToString(Read["EMAIL"]);
                        employee.Customer = Convert.ToString(Read["CUSTOMER"]);
                        employee.Position = Convert.ToString(Read["POSITION"]);
                        employee.ProfileId = Convert.ToInt32(Read["ID_PROFILE"]);
                        employee.ManagerId = Convert.ToInt32(Read["ID_MANAGER"]);
                        employee.HireDate = Convert.ToDateTime(Read["HIRE_DATE"]);
                        employee.Ranking = Convert.ToInt32(Read["RANKING"]);
                        string endDate = Convert.ToString(Read["END_DATE"]);

                        if (!string.IsNullOrEmpty(endDate))
                        {
                            employee.EndDate = Convert.ToDateTime(endDate);
                        }
                        else
                        {
                            employee.EndDate = null;
                        }
                        employees.Add(employee);
                    }
                    db.Close();
                }

            }
            catch
            {
                employees = null;
            }
            return employees;
        }

        // Insert a employee data in the DB
        public bool InsertEmployee(Employee employee)
        {
            bool status = false;

            // Connect to the DB 
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                db.Open();
                // insert
                try
                {
                    string InsertQuery = "INSERT INTO EMPLOYEE (FIRST_NAME," +
                                                               "LAST_NAME," +
                                                               "EMAIL," +
                                                               "CUSTOMER," +
                                                               "POSITION," +
                                                               "ID_PROFILE,"+
                                                               "ID_MANAGER," +
                                                               "HIRE_DATE," +
                                                               "RANKING," +
                                                               "END_DATE)" +
                                     " VALUES ('" + employee.FirstName + "', '" +
                                               employee.LastName + "', '" +
                                               employee.Email + "', '" +
                                               employee.Customer + "', '" +
                                               employee.Position + "', '" +
                                               employee.ProfileId+ "', '" +
                                               employee.ManagerId + "', '" +
                                               employee.HireDate.ToShortDateString() + "', '" +
                                               employee.Ranking + "', '" +
                                               (employee.EndDate.HasValue ? employee.EndDate.Value.ToShortDateString() : null) + "')";

                    OracleCommand Comand = new OracleCommand(InsertQuery, db);
                    Comand.ExecuteNonQuery();

                    status = true;
                }
                catch
                {
                    status = false;
                }
                db.Close();
            }
            return status;
        }


        //Get ID_Profile from the DB 
        public string GetUserProfile(string userEmail)
        {
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string QueryProfile = "SELECT ID_PROFILE FROM EMPLOYEE WHERE EMAIL=" + "'" + userEmail + "'";
                    string QueryName = "SELECT FIST_NAME FROM EMPLOYEE WHERE EMAIL=" + "'" + userEmail + "'";
                    OracleCommand Comand = new OracleCommand(QueryProfile, db);
                    OracleDataReader Read = Comand.ExecuteReader();

                    while (Read.Read())
                    {
                        Profile = Convert.ToString(Read["ID_PROFILE"]);
                    }
                    Comand = new OracleCommand(QueryName, db);
                    Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {
                        Name = Convert.ToString(Read["FIRST_NAME"]);
                    }
                    db.Close();
                }

                return Profile;
            }
            catch
            {
                return "0";
            }
        }

        //Get User Name to show in the Resource view 
        public string UserName()
        {
            return Name;
        }

        //Get Users Name By Manager Id
        public List<Employee> GetEmployeeByManager(int ManageerId)
        {
            List<Employee> Employees = new List<Employee>();
            Employee employee = new Employee();
            try
            {
                using(OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string GetEmployees = "SELECT ID_EMPLOYEE, "+ 
                                       "FIRST_NAME, " +
                                       "LAST_NAME, " +
                                       "EMAIL, " +
                                       "CUSTOMER, " +
                                       "POSITION, " +
                                       "ID_PROFILE, "+
                                       "ID_MANAGER, " +
                                       "HIRE_DATE, " +
                                       "RANKING, " +
                                       "END_DATE " +
                                       "FROM EMPLOYEE WHERE ID_MANAGER = "+ ManageerId;
                    OracleCommand Command = new OracleCommand(GetEmployees, db);
                    Command.ExecuteNonQuery();
                    OracleDataReader Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        employee = new Employee();
                        employee.EmployeeId = Convert.ToInt32(Reader["ID_EMPLOYEE"]);
                        employee.FirstName = Convert.ToString(Reader["FIRST_NAME"]);
                        employee.LastName = Convert.ToString(Reader["LAST_NAME"]);
                        employee.Email = Convert.ToString(Reader["EMAIL"]);
                        employee.Customer = Convert.ToString(Reader["CUSTOMER"]);
                        employee.Position = Convert.ToString(Reader["POSITION"]);
                        employee.ManagerId = Convert.ToInt32(Reader["ID_PROFILE"]);
                        employee.ManagerId = Convert.ToInt32(Reader["ID_MANAGER"]);
                        employee.HireDate = Convert.ToDateTime(Reader["HIRE_DATE"]);
                        employee.Ranking = Convert.ToInt32(Reader["RANKING"]);
                        string endDate = Convert.ToString(Reader["END_DATE"]);

                        if (!string.IsNullOrEmpty(endDate))
                        {
                            employee.EndDate = Convert.ToDateTime(endDate);
                        }
                        else
                        {
                            employee.EndDate = null;
                        }
                        Employees.Add(employee);
                    }
                    db.Close();
                }

            }
            catch
            {
                Employees = null;
            }
            return Employees;
        }

        /// <summary>
        /// Function to get the employees from the XLS Resource file 
        /// </summary>
        /// <param name="pathFileString">Path string of the file to be loaded</param>
        /// <returns>Returns a list of employees</returns>
        public List<Employee> GetEmployeesFromXLSFile(string pathFileString)
        {
            //Declare variables
            List<Employee> employees = new List<Employee>();

            #region Load XLS file with EPPlus
            //Validate path file 
            if (!String.IsNullOrEmpty(pathFileString))
            {
                FileInfo file;

                try
                {
                    //Creates a new file
                    file = new FileInfo(pathFileString);

                    //Excel file valid extensions
                    var validExtensions = new string[] { ".xls", ".xlsx" };

                    //Validate extesion of file selected
                    if (!validExtensions.Contains(file.Extension))
                    {
                        throw new System.IO.FileFormatException("Excel file not found in specified path");
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

                // Open and read the XlSX file.
                ExcelPackage package = null;
                try
                {
                    package = new ExcelPackage(file);
                }
                catch (System.IO.FileFormatException ex)
                {
                    throw new System.IO.FileFormatException("Unable to read excel file");
                }
                catch (Exception ex)
                {
                    throw;
                }

                using (package)
                {
                    // Get the work book in the file
                    ExcelWorkbook workBook = package.Workbook;

                    //If there is a workbook
                    if (workBook != null && workBook.Worksheets.Count > 0)
                    {
                        //Declare inner variables
                        IEnumerable<string> columnNames;
                        int countRows = 0;
                        bool required = false;

                        // Change required columns if needed
                        List<string> requiredColumns = ResourceColumns.GetColumnList();

                        // Get the first worksheet
                        ExcelWorksheet sheet = workBook.Worksheets.First();

                        //Get column names in the file
                        columnNames = sheet.GetSheetColumnNames();

                        //Check if file contains the required columns
                        required = columnNames.ContainsAll(requiredColumns);

                        //If file has required columns
                        if (required)
                        {
                            //Get columns 
                            var columns = sheet.GetSheetColumns();

                            //Get count of rows
                            countRows = sheet.GetCountRowsFromColumn(countColumn, columns);

                            //Read and store data
                            var columnsData = new Dictionary<string, IEnumerable<string>>();
                            foreach (var colName in columnNames)
                            {
                                columnsData.Add(colName, sheet.GetRowsFromHeader(colName, columns));
                            }

                            //--[ Starts file processing
                            #region File processing

                            //create list of employees
                            Employee newEmployee;
                            employees = new List<Employee>();

                            //Read each employee in the file
                            for (int i = 0; i < countRows; i++)
                            {
                                // Create new employee for each record in the file
                                newEmployee = new Employee();

                                // Get required fields
                                newEmployee.FirstName = columnsData[ResourceColumns.FirstName].ToArray<string>()[i];
                                newEmployee.LastName = columnsData[ResourceColumns.LastName].ToArray<string>()[i];
                                newEmployee.Email = columnsData[ResourceColumns.Email].ToArray<string>()[i];
                                newEmployee.Customer = "No Customer"; // Column not comming from excel file
                                newEmployee.Position = columnsData[ResourceColumns.JobCode].ToArray<string>()[i];

                                #region Set profile
                                // --[ Set profile
                                var user = columnsData[ResourceColumns.Email].ToArray<string>()[i];

                                if (!string.IsNullOrEmpty(user))
                                {
                                    if(user == ProfilesOwners.Director)
                                    {
                                        // User is director
                                        newEmployee.ProfileId = (int)ProfileUser.Director;
                                    }
                                    else if (ProfilesOwners.Managers.Contains(user))
                                    {
                                        // User is manager
                                        newEmployee.ProfileId = (int)ProfileUser.Manager;
                                    }
                                    else 
                                    {
                                        // User is resource
                                        newEmployee.ProfileId = (int)ProfileUser.Resource;
                                    }
                                }
                                else 
                                {
                                    newEmployee.ProfileId = (int)ProfileUser.None; 
                                }
                                // --] Set profile
                                #endregion

                                newEmployee.ManagerId = 2; // Set as 2 for now: Create a function to update later when table is already populated
                                newEmployee.HireDate = DateTime.Now; // Set as today due to not comming from excel
                                newEmployee.Ranking = 0;

                                var active = columnsData[ResourceColumns.Active].ToArray<string>()[i];
                                if (string.IsNullOrEmpty(active))
                                {
                                    // Employee is still active
                                    newEmployee.EndDate = null;
                                }
                                else 
                                {
                                    newEmployee.EndDate = DateTime.Now;
                                }
                                
                                newEmployee.Project = "No project"; // Column not comming from excel file

                                //Add employee to the list 
                                employees.Add(newEmployee);

                            } //End for loop
                            #endregion
                            //--] Ends file processing

                        }//Enf if requiredFields
                        else
                        {
                            //File does not have the required columns: 
                            throw new ApplicationException("File does not have the required columns");
                        }

                    }//Enf if workbook != null
                    else
                    {
                        //No worksheets found
                        throw new ApplicationException("No worksheets found on Excel file");
                    }
                    //}
                }//End using
            }
            #endregion

            return employees;
        }

        public List<Employee> UpdateManagerAssigment(List<Employee> employees) 
        {
            // Add code 

            return employees;
        }
    }
}