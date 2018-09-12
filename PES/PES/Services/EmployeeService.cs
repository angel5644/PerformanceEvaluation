 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using OfficeOpenXml;
using System.IO;
using System.Data.SqlClient;
using System.Data;

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
                                           "END_DATE, " +
                                           "PROJECT, " +
                                           "ID_LOCATION, " +
                                           "FREE_DAYS " +
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
                        string endDate = Convert.ToString(Read["END_DATE"]);
                        employee.Project = Convert.ToString(Read["PROJECT"]);
                        employee.LocationId = Convert.ToInt32(Read["ID_LOCATION"]);
                        employee.Freedays = Convert.ToInt32(Read["FREE_DAYS"]);

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
            catch (Exception ex)
            {
                throw;
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
                                           "END_DATE, " +
                                           "PROJECT, " +
                                           "ID_LOCATION, " +
                                           "FREE_DAYS " +
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
                        string endDate = Convert.ToString(Read["END_DATE"]);
                        employee.Project = Convert.ToString(Read["PROJECT"]);
                        employee.LocationId = Convert.ToInt32(Read["ID_LOCATION"]);
                        employee.Freedays = Convert.ToInt32(Read["FREE_DAYS"]);

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
            catch (Exception ex)
            {
                throw;
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
                                           "END_DATE, " +
                                           "PROJECT, " +
                                           "ID_LOCATION, " +
                                           "FREE_DAYS, " +
                                           "REMINDED_DATE " +
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
                        string endDate = Convert.ToString(Read["END_DATE"]);
                        employee.Project = Convert.ToString(Read["PROJECT"]);
                        employee.LocationId = Convert.ToInt32(Read["ID_LOCATION"]);
                        employee.Freedays = Convert.ToInt32(Read["FREE_DAYS"]);
                       // employee.ReminderDate = Convert.ToDateTime(Read["REMINDED_DATE"]);
                        string ReminderDate = Convert.ToString(Read["REMINDED_DATE"]);
                        if (!string.IsNullOrEmpty(ReminderDate))
                        {
                            employee.ReminderDate = Convert.ToDateTime(ReminderDate);
                        }
                        else
                        {
                            employee.ReminderDate = null;
                        }

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
            catch (Exception ex)
            {
                throw;
            }
            return employees;
        }

        //Get employees by porfile id
        public List<Employee> GetByPorfileId(int porfileId)
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
                                           "END_DATE, " +
                                           "PROJECT, " +
                                           "ID_LOCATION " +
                                           "FROM EMPLOYEE " +
                                           "WHERE ID_PROFILE = '" +
                                           porfileId + "'";

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
                        string endDate = Convert.ToString(Read["END_DATE"]);
                        employee.Project = Convert.ToString(Read["PROJECT"]);
                        employee.LocationId = Convert.ToInt32(Read["ID_LOCATION"]);

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
            catch (Exception ex)
            {
                throw;
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

                #region Old insert
                //string InsertQuery = "INSERT INTO EMPLOYEE (FIRST_NAME," +
                //                                           "LAST_NAME," +
                //                                           "EMAIL," +
                //                                           "CUSTOMER," +
                //                                           "POSITION," +
                //                                           "ID_PROFILE,"+
                //                                           "ID_MANAGER," +
                //                                           "HIRE_DATE," +
                //                                           "RANKING," +
                //                                           "END_DATE)" +
                //                 " VALUES ('" + employee.FirstName + "', '" +
                //                           employee.LastName + "', '" +
                //                           employee.Email + "', '" +
                //                           employee.Customer + "', '" +
                //                           employee.Position + "', '" +
                //                           employee.ProfileId+ "', '" +
                //                           employee.ManagerId + "', '" +
                //                           employee.HireDate.ToShortDateString() + "', '" +
                //                           employee.Ranking + "', '" +
                //                           (employee.EndDate.HasValue ? employee.EndDate.Value.ToShortDateString() : null) + "')";

                //SqlParameter x = new SqlParameter();
                //x.DbType = System.Data.DbType.

                //OracleCommand Comand = new OracleCommand(InsertQuery, db);
                //Comand.ExecuteNonQuery();
                #endregion

                #region New insert
                string insertQuery = @" INSERT INTO EMPLOYEE (  FIRST_NAME,
                                                                LAST_NAME,
                                                                EMAIL,
                                                                CUSTOMER,
                                                                POSITION,
                                                                ID_PROFILE,
                                                                ID_MANAGER,
                                                                END_DATE,
                                                                ID_LOCATION) 

                                                        VALUES (:firstName,
                                                                :lastName,
                                                                :email,
                                                                :customer,
                                                                :position,
                                                                :idProfile,
                                                                :idManager,
                                                                :endDate,
                                                                :locationId   
                                                                )";

                // Adding parameters
                using (OracleCommand command = new OracleCommand(insertQuery, db))
                {
                    command.Parameters.Add(new OracleParameter("firstName", employee.FirstName));
                    command.Parameters.Add(new OracleParameter("lastName", employee.LastName));
                    command.Parameters.Add(new OracleParameter("email", employee.Email));
                    command.Parameters.Add(new OracleParameter("customer", employee.Customer));
                    command.Parameters.Add(new OracleParameter("position", employee.Position));
                    command.Parameters.Add(new OracleParameter("idProfile", employee.ProfileId));
                    command.Parameters.Add(new OracleParameter("idManager", employee.ManagerId));
                    command.Parameters.Add(new OracleParameter("endDate", OracleDbType.Date, employee.EndDate, ParameterDirection.Input));
                    command.Parameters.Add(new OracleParameter("locationId", employee.LocationId));

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }

                    status = true;
                }
                #endregion
            }
            return status;
        }


        // temporal service from remindedemployee

        public bool InsertReminderEmployee(Employee employee)
        {
            bool status = false;

            // Connect to the DB 
            using (OracleConnection db = dbContext.GetDBConnection())
            {

                #region Old insert
               
                #endregion

                #region New insert
                string insertQuery = @" INSERT INTO REMINDER_DATES (ID_EMPLOYEE,
                                                                REMINDER_DATE) 

                                                        VALUES (:employee_id,                                                                
                                                                TO_DATE(:reminder_date,'dd/mm/yyyy hh24:mi:ss')  
                                                                )";

                // Adding parameters
                using (OracleCommand command = new OracleCommand(insertQuery, db))
                {
                    command.Parameters.Add(new OracleParameter("employee_id", employee.EmployeeId));
                    command.Parameters.Add(new OracleParameter("reminder_date", OracleDbType.Date, employee.ReminderDate, ParameterDirection.Input));

                    try
                    {
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                        command.Connection.Close();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.ToString());
                        throw;
                    }

                    status = true;
                }
                #endregion
            }
            return status;
        }




        public bool UpdateEmployee(Employee employee) 
        {
            try
            {
                var user = this.GetByID(employee.EmployeeId);

                if(user != null)
                {
                    using (OracleConnection db = dbContext.GetDBConnection())
                    {
                        db.Open();

                        //string InsertQuery =  "UPDATE EMPLOYEE SET EMAIL='" + employee.Email + "', " +
                        //                                       "CUSTOMER='" + employee.Customer + "', " +
                        //                                       "POSITION='" + employee.Position + "', " +
                        //                                       "ID_PROFILE='" + employee.ProfileId + "', " +
                        //                                       "ID_MANAGER='" + employee.ManagerId + "', " +
                        //                                       "HIRE_DATE= TO_DATE('" + employee.HireDate.ToString("MM-dd-yyyy") + "', 'MM-DD-YYYY'), " +
                        //                                       "END_DATE= TO_DATE('" + (employee.EndDate.HasValue ? employee.EndDate.Value.ToString("MM-dd-yyyy") : "") + "', 'MM-DD-YYYY'), " +
                        //                                       "PROJECT='" + employee.Project +"' " +
                        //             "WHERE ID_EMPLOYEE='" + employee.EmployeeId + "'";

                        string InsertQuery = "UPDATE EMPLOYEE SET FIRST_NAME='" + employee.FirstName + "', " +
                                                               "LAST_NAME='" + employee.LastName + "', " +
                                                               "EMAIL='" + employee.Email + "', " +
                                                               "ID_PROFILE='" + employee.ProfileId + "', " +
                                                               "END_DATE= TO_DATE('" + (employee.EndDate.HasValue ? employee.EndDate.Value.ToString("MM-dd-yyyy") : "") + "', 'MM-DD-YYYY'), " +
                                                               "ID_MANAGER='" + employee.ManagerId + "', " +
                                                               "ID_LOCATION='" + employee.LocationId + "' " +
                                     "WHERE ID_EMPLOYEE='" + employee.EmployeeId + "'";

                        OracleCommand Comand = new OracleCommand(InsertQuery, db);
                        Comand.ExecuteNonQuery();

                        db.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return true;
        }


        // reminded service

        public bool UpdateEmployeeRemindedDate(Employee employee)
        {
            try
            {
                var user = this.GetByID(employee.EmployeeId);

                if (user != null)
                {
                    using (OracleConnection db = dbContext.GetDBConnection())
                    {
                        db.Open();


                        string InsertQuery = "UPDATE EMPLOYEE SET REMINDED_DATE = '" + employee.ReminderDate.ToString() + "' " +
                                     "WHERE ID_EMPLOYEE='" + employee.EmployeeId + "'";
                        OracleCommand Comand = new OracleCommand(InsertQuery, db);
                        Comand.ExecuteNonQuery();

                        db.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }







        public bool TransferAllEmployees(int manager, int newManager)
        {
            try
            {
                var user = this.GetByID(manager);

                if (user != null)
                {
                    using (OracleConnection db = dbContext.GetDBConnection())
                    {
                        db.Open();

                        string InsertQuery = "UPDATE EMPLOYEE SET ID_MANAGER='" + newManager + "' " +                                                          
                                     "WHERE ID_MANAGER='" + manager + "'";

                        OracleCommand Comand = new OracleCommand(InsertQuery, db);
                        Comand.ExecuteNonQuery();

                        db.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
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
                    string QueryName = "SELECT FIRST_NAME FROM EMPLOYEE WHERE EMAIL=" + "'" + userEmail + "'";
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Employee> GetEmployeesByProfile(int userId, int profileId)
        {
            List<Employee> employees = new List<Employee>();
            Employee employee = new Employee();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    if (userId == (int)ProfileUser.Manager)
                    {
                        string Query = "SELECT ID_EMPLOYEE, " +
                                               "FIRST_NAME, " +
                                               "LAST_NAME, " +
                                               "EMAIL, " +
                                               "ID_PROFILE," +
                                               "ID_MANAGER, " +
                                               "END_DATE, " +
                                               "ID_LOCATION " +
                                               "FROM EMPLOYEE " +
                                               "WHERE ID_MANAGER = '" +
                                               userId + "'";
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
                            employee.ProfileId = Convert.ToInt32(Read["ID_PROFILE"]);
                            employee.ManagerId = Convert.ToInt32(Read["ID_MANAGER"]);
                            string endDate = Convert.ToString(Read["END_DATE"]);
                            employee.LocationId = Convert.ToInt32(Read["ID_LOCATION"]);

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
                    }
                    else
                    {
                        var DirectorsEmployees = GetEmployeesByDirector(userId);
                        foreach (var item in DirectorsEmployees)
                        {
                            employees.Add(item);
                        }
                    }
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return employees;
        }

        public bool InsertDirector(Employee employee)
        {
            try
            {
                var user = this.GetByID(employee.EmployeeId);

                if (user != null)
                {
                    using (OracleConnection db = dbContext.GetDBConnection())
                    {
                        db.Open();

                        string InsertQuery = "UPDATE EMPLOYEE SET ID_MANAGER='" + employee.EmployeeId + "' " +
                                     "WHERE ID_EMPLOYEE='" + employee.EmployeeId + "'";

                        OracleCommand Comand = new OracleCommand(InsertQuery, db);
                        Comand.ExecuteNonQuery();

                        db.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        public List<Employee> GetEmployeesByDirector(int DirectorId)
        {
            List<Employee> Employees = new List<Employee>();
            Employee employee = new Employee();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string GetEmployees = "SELECT ID_EMPLOYEE, " +
                                       "FIRST_NAME, " +
                                       "LAST_NAME, " +
                                       "EMAIL, " +
                                       "POSITION, " +
                                       "ID_PROFILE, " +
                                       "ID_MANAGER, " +
                                       "END_DATE, " +
                                       "ID_LOCATION " +
                                       "FROM EMPLOYEE WHERE ID_MANAGER IN " +
                                       "(SELECT ID_EMPLOYEE " +
                                       "FROM EMPLOYEE WHERE ID_MANAGER = " + DirectorId + " OR ID_EMPLOYEE = " + DirectorId + ")";
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
                        employee.Position = Convert.ToString(Reader["POSITION"]);
                        employee.ProfileId = Convert.ToInt32(Reader["ID_PROFILE"]);
                        employee.ManagerId = Convert.ToInt32(Reader["ID_MANAGER"]);
                        string endDate = Convert.ToString(Reader["END_DATE"]);
                        employee.LocationId = Convert.ToInt32(Reader["ID_LOCATION"]);

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
            catch (Exception ex)
            {
                throw;
            }
            return Employees;
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
                                       "END_DATE, " +
                                       "PROJECT, " +
                                       "ID_LOCATION " +
                                       "FROM EMPLOYEE WHERE ID_MANAGER = " + ManageerId + " OR ID_EMPLOYEE = " + ManageerId;
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
                        employee.ProfileId = Convert.ToInt32(Reader["ID_PROFILE"]);
                        employee.ManagerId = Convert.ToInt32(Reader["ID_MANAGER"]);
                        string endDate = Convert.ToString(Reader["END_DATE"]);
                        employee.Project = Convert.ToString(Reader["PROJECT"]);
                        employee.LocationId = Convert.ToInt32(Reader["ID_LOCATION"]);

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
            catch (Exception ex)
            {
                throw;
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
                               /* newEmployee.HireDate = DateTime.Now; */// Set as today due to not comming from excel

                                var active = columnsData[ResourceColumns.Active].ToArray<string>()[i];
                                    newEmployee.EndDate = null;

                                #region Validation to check if active
                                // Uncomment if validation will be applied, otherwhise employees will be active
                                //if (!string.IsNullOrEmpty(active) && active.ToLower() == "no")
                                //{
                                //    newEmployee.EndDate = DateTime.Now;
                                //}
                                #endregion
                                
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

        public bool TransferEmployees(int employeeId, int managerId)
        {
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                   db.Open();
                   string UpdateQuery = "UPDATE EMPLOYEE SET ID_MANAGER='" + managerId + "' " +
                                     "WHERE ID_EMPLOYEE='" + employeeId + "'";
                    OracleCommand Command = new OracleCommand(UpdateQuery, db);
                    Command.ExecuteNonQuery();
                    OracleDataReader Reader = Command.ExecuteReader();

                    db.Close();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        // Get employees by location
        public List<Employee> GetEmployeesByLocation(int locationId)
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
                                           "END_DATE, " +
                                           "PROJECT, " +
                                           "LOCATION_ID " +
                                           "FROM EMPLOYEE " +
                                           "WHERE ID_LOCATION = '" +
                                           locationId + "'";

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
                        string endDate = Convert.ToString(Read["END_DATE"]);
                        employee.Project = Convert.ToString(Read["PROJECT"]);
                        employee.LocationId = Convert.ToInt32(Read["ID_LOCATION"]);

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
            catch (Exception ex)
            {
                throw;
            }
            return employees;
        }




        //////public Employee GetLeadByEmployeeId(int employeedId)
        //////{
        ////    Lead name del ultimo request por empleado
        //////}
    }
}