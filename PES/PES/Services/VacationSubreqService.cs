using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using PES.ViewModels;


namespace PES.Services
{
    public class VacationSubreqService
    {
        string MesCorrectoAuto;
        private PESDBContext dbContext = new PESDBContext();
        public VacationSubreqService()
        {
            dbContext = new PESDBContext();
        }

        /// <summary>
        /// Metod to GET all subRequests of a request by RequestId 
        /// </summary>
        /// <param name="headerId"></param>
        /// <returns>A list of Sub Resquests</returns>
        public List<VacationSubreq> GetVacationSubreqByHeaderReqId(int headerId)
        {
            List<VacationSubreq> vacationSubReqs = new List<VacationSubreq>();
            VacationSubreq vacationSubReq = new VacationSubreq();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string query = "SELECT " +
                                        "ID_SUBREQ, " +
                                        "ID_HEADER_REQ, " +
                                        "START_DATE, " +
                                        "END_DATE," +
                                        "RETURN_DATE, " +
                                        "HAVE_PROJECT, " +
                                        "LEAD_NAME " +
                                    "FROM " +
                                        "VACATION_SUBREQ " +
                                    "WHERE " +
                                        "ID_HEADER_REQ = :headerId " +
                                    "ORDER BY ID_SUBREQ ASC";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("headerId", headerId));
                        command.ExecuteReader();
                        OracleDataReader Reader = command.ExecuteReader();
                        while (Reader.Read())
                        {
                            vacationSubReq = new VacationSubreq();
                            vacationSubReq.SubreqId = Convert.ToInt32(Reader["ID_SUBREQ"]);
                            vacationSubReq.VacationHeaderReqId = Convert.ToInt32(Reader["ID_HEADER_REQ"]);
                            vacationSubReq.StartDate = Convert.ToDateTime(Reader["START_DATE"]);
                            vacationSubReq.EndDate = Convert.ToDateTime(Reader["END_DATE"]);
                            vacationSubReq.ReturnDate = Convert.ToDateTime(Reader["RETURN_DATE"]);
                            vacationSubReq.HaveProject = Convert.ToString(Reader["HAVE_PROJECT"]);
                            vacationSubReq.LeadName = Convert.ToString(Reader["LEAD_NAME"]);
                            vacationSubReqs.Add(vacationSubReq); 
                        }
                    }

                    db.Close();
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            return vacationSubReqs;
        }

        /// <summary>
        /// Method to get the count of request by year and location 
        /// </summary>
        /// <param name="year", name="location"></param>
        /// <returns>A list of Sub Resquests</returns>
        public List<Charts> GetVacationsForChart(int year, string location)
        {
            List<ChartRequestsViewModel> vacationSubReqs = new List<ChartRequestsViewModel>();
            ChartRequestsViewModel vacationSubReq = new ChartRequestsViewModel();
            List<Charts> charts = new List<Charts>();

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    //STRING to get vacations requests by month 
                    //string query = "WITH requests AS (SELECT COUNT(ID_SUBREQ) VACATIONS,EXTRACT(MONTH FROM DATE_CREATED) DATE_CREATED" +
                    //    " FROM PE.VACATION_SUBREQ sub" +
                    //    " INNER JOIN PE.VACATION_HEADER_REQ hdr ON sub.ID_HEADER_REQ = hdr.ID_HEADER_REQ" +
                    //    " INNER JOIN PE.EMPLOYEE emp ON hdr.ID_EMPLOYEE = emp.ID_EMPLOYEE" +
                    //    " INNER JOIN PE.LOCATION loc ON emp.ID_LOCATION = loc.ID_LOCATION" +
                    //    " WHERE loc.NAME = '" + location +"' AND EXTRACT(YEAR FROM sub.DATE_CREATED) = "+ year +
                    //    " GROUP BY EXTRACT(MONTH FROM DATE_CREATED))" +
                    //    " SELECT MONTH_VALUE, MONTH_DISPLAY MONTH, NVL(VACATIONS, 0) VACATIONS FROM requests" +
                    //    " RIGHT JOIN WWV_FLOW_MONTHS_MONTH syst ON  DATE_CREATED = syst.MONTH_VALUE" +
                    //    " GROUP BY VACATIONS, MONTH_VALUE, MONTH_DISPLAY, NVL(VACATIONS, 0)" +
                    //    " ORDER BY syst.MONTH_VALUE";
                    string query = "SELECT" +
                        " SUB.START_DATE, SUB.END_DATE, HDR.NO_VAC_DAYS" +
                        " FROM PE.VACATION_SUBREQ SUB" +
                        " INNER JOIN PE.VACATION_HEADER_REQ HDR ON SUB.ID_HEADER_REQ = HDR.ID_HEADER_REQ" +
                        " INNER JOIN PE.EMPLOYEE EMP ON HDR.ID_EMPLOYEE = EMP.ID_EMPLOYEE" +
                        " INNER JOIN PE.LOCATION LOC ON EMP.ID_LOCATION = LOC.ID_LOCATION" +
                        " WHERE EXTRACT(YEAR FROM START_DATE) = :year AND LOC.NAME = :location AND HDR.ID_REQ_STATUS = 3";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("year", OracleDbType.Int32, year, System.Data.ParameterDirection.Input));
                        command.Parameters.Add(new OracleParameter("location", OracleDbType.Varchar2, 25, location, System.Data.ParameterDirection.Input));
                        command.ExecuteReader();
                        OracleDataReader Reader = command.ExecuteReader();
                        while (Reader.Read())
                        {
                            vacationSubReq = new ChartRequestsViewModel();
                            vacationSubReq.StartDate = Convert.ToDateTime(Reader["START_DATE"]);
                            vacationSubReq.EndDate = Convert.ToDateTime(Reader["END_DATE"]);
                            vacationSubReq.VacationDays = Convert.ToInt32(Reader["NO_VAC_DAYS"]);
                            vacationSubReqs.Add(vacationSubReq);
                        }
                    }

                    db.Close();
                }
                //Calculation
                int[] daysByMonth = new int[12];
                CalculateDaysByMonth(vacationSubReqs, year, daysByMonth);

                //Filling charts list
                int monthNumber = 1;
                DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
                string monthName;
                foreach (var days in daysByMonth)
                {
                    monthName = dateTimeFormatInfo.GetMonthName(monthNumber);
                    charts.Add(new Charts() { Month = monthName, VacationDays = days });
                    monthNumber++;
                }
            }

            catch (Exception ex)
            {
                throw;
            }

            return charts;
        }

        /// <summary>
        /// Metod to INSERT a subrequest in the DB using a VacationSubreq object
        /// </summary>
        /// <param name="vacSubReq"></param>
        /// <returns>True if the insert was successful</returns>
        /// 


        public bool InsertSubReq(int idHeaderReq, List<SubrequestInfoVM> model)
        {
            bool status = false;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string query = "INSERT INTO PE.VACATION_SUBREQ" +
                    "(" +
                    "ID_HEADER_REQ," +
                    "START_DATE," +
                    "END_DATE," +
                    "RETURN_DATE," +
                    "HAVE_PROJECT," +
                    "LEAD_NAME)" +
                     "VALUES" +
                    "(" +
                    " :IdHeaderReq, " +
                    " :StartDate," +
                    " :EndDate," +
                    " :ReturnDate," +
                    " :HaveProject," +
                    " :LeadName)";
                

                using (OracleCommand command = new OracleCommand(query, db))
                {
                    try
                    {
                      
                        command.Connection.Open();
                        foreach (var date in model)
                        {
                            string returnDate =date.ReturnDate;
                            string rMonth = returnDate.Substring(0, 2);
                            switch (rMonth)
                            {
                                case "01":
                                    {
                                        MesCorrectoAuto = "JAN";
                                        break;
                                    }
                                case "02":
                                    {
                                        MesCorrectoAuto = "FEB";
                                        break;
                                    }
                                case "03":
                                    {
                                        MesCorrectoAuto = "MAR";
                                        break;
                                    }
                                case "04":
                                    {
                                        MesCorrectoAuto = "APR";
                                        break;
                                    }
                                case "05":
                                    {
                                        MesCorrectoAuto = "MAY";
                                        break;
                                    }
                                case "06":
                                    {
                                        MesCorrectoAuto = "JUN";
                                        break;
                                    }
                                case "07":
                                    {
                                        MesCorrectoAuto = "JUL";
                                        break;
                                    }
                                case "08":
                                    {
                                        MesCorrectoAuto = "AUG";
                                        break;
                                    }
                                case "09":
                                    {
                                        MesCorrectoAuto = "SEP";
                                        break;
                                    }
                                case "10":
                                    {
                                        MesCorrectoAuto = "OCT";
                                        break;
                                    }
                                case "11":
                                    {
                                        MesCorrectoAuto = "NOV";
                                        break;
                                    }
                                case "12":
                                    {
                                        MesCorrectoAuto = "DEC";
                                        break;
                                    }
                            }
                            string rDay = returnDate.Substring(3, 2);
                            string rYear = returnDate.Substring(6, 4);
                            string FinalReturnDate = (rDay + "/" + MesCorrectoAuto + "/" + rYear);

                            command.Parameters.Add(new OracleParameter("IdHeaderReq", idHeaderReq));
                            command.Parameters.Add(new OracleParameter("StartDate", date.StartDate));
                            command.Parameters.Add(new OracleParameter("EndDate", date.EndDate));
                            command.Parameters.Add(new OracleParameter("ReturnDate", FinalReturnDate));
                            //get employee
                            EmployeeService employeeService = new EmployeeService();
                            var leadnameFirstName = employeeService.GetByID(date.SelectedEmployee).FirstName;
                            var leadnameLastName = employeeService.GetByID(date.SelectedEmployee).LastName;
                            var leadnameWholeName = leadnameFirstName + " " + leadnameLastName;

                            command.Parameters.Add(new OracleParameter("HaveProject", (date.HaveProject).ToString()));
                            command.Parameters.Add(new OracleParameter("LeadName", leadnameWholeName));  
                            

                            command.ExecuteNonQuery();
                        }

                        command.Connection.Close();
                    }
                    catch (OracleException ex)
                    {
                        throw;
                    }
                    status = true;
                }
            }
            return status;
        }

        //New methot to insert the subrequest
        public bool InsertSubrequest(int idHeaderReq, SubrequestInfoVM subrequest)
        {

            bool status = false;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string query = "INSERT INTO PE.VACATION_SUBREQ" +
                    "(" +
                    "ID_HEADER_REQ," +
                    "START_DATE," +
                    "END_DATE," +
                    "RETURN_DATE," +
                    "HAVE_PROJECT," +
                    "LEAD_NAME)" +
                     "VALUES" +
                    "(" +
                    " :IdHeaderReq, " +
                    " :StartDate," +
                    " :EndDate," +
                    " :ReturnDate," +
                    " :HaveProject," +
                    " :LeadName)";


                using (OracleCommand command = new OracleCommand(query, db))
                {
                    try
                    {

                        command.Connection.Open();

                            string returnDate = subrequest.ReturnDate;
                            string rMonth = returnDate.Substring(0, 2);
                            switch (rMonth)
                            {
                                case "01":
                                    {
                                        MesCorrectoAuto = "JAN";
                                        break;
                                    }
                                case "02":
                                    {
                                        MesCorrectoAuto = "FEB";
                                        break;
                                    }
                                case "03":
                                    {
                                        MesCorrectoAuto = "MAR";
                                        break;
                                    }
                                case "04":
                                    {
                                        MesCorrectoAuto = "APR";
                                        break;
                                    }
                                case "05":
                                    {
                                        MesCorrectoAuto = "MAY";
                                        break;
                                    }
                                case "06":
                                    {
                                        MesCorrectoAuto = "JUN";
                                        break;
                                    }
                                case "07":
                                    {
                                        MesCorrectoAuto = "JUL";
                                        break;
                                    }
                                case "08":
                                    {
                                        MesCorrectoAuto = "AUG";
                                        break;
                                    }
                                case "09":
                                    {
                                        MesCorrectoAuto = "SEP";
                                        break;
                                    }
                                case "10":
                                    {
                                        MesCorrectoAuto = "OCT";
                                        break;
                                    }
                                case "11":
                                    {
                                        MesCorrectoAuto = "NOV";
                                        break;
                                    }
                                case "12":
                                    {
                                        MesCorrectoAuto = "DEC";
                                        break;
                                    }
                            }
                            string rDay = returnDate.Substring(3, 2);
                            string rYear = returnDate.Substring(6, 4);
                            string FinalReturnDate = (rDay + "/" + MesCorrectoAuto + "/" + rYear);

                            command.Parameters.Add(new OracleParameter("IdHeaderReq", idHeaderReq));
                            command.Parameters.Add(new OracleParameter("StartDate", subrequest.StartDate));
                            command.Parameters.Add(new OracleParameter("EndDate", subrequest.EndDate));
                            command.Parameters.Add(new OracleParameter("ReturnDate", FinalReturnDate));
                            //get employee
                            EmployeeService employeeService = new EmployeeService();
                            var leadnameFirstName = employeeService.GetByID(subrequest.SelectedEmployee).FirstName;
                            var leadnameLastName = employeeService.GetByID(subrequest.SelectedEmployee).LastName;
                            var leadnameWholeName = leadnameFirstName + " " + leadnameLastName;

                            command.Parameters.Add(new OracleParameter("HaveProject", (subrequest.HaveProject).ToString()));
                            command.Parameters.Add(new OracleParameter("LeadName", leadnameWholeName));


                            command.ExecuteNonQuery();
                        

                        command.Connection.Close();
                    }
                    catch (OracleException ex)
                    {
                        throw;
                    }
                    status = true;
                }
            }
            return status;
        }
        //New methot to insert the subrequest


        public int GetHeaderRequest(SendRequestViewModel data)
        {
            int iDHeaderReq = 0;
            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    
                    string query =  "select   max (id_header_req) as    \"currentHeaderReq\"" + 
                                    "from      PE.VACATION_HEADER_REQ " +
                                    "where     id_employee = '" + data.EmployeedID + "'" +
                                    "order by  id_header_req  asc ";

                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            iDHeaderReq = Convert.ToInt32(reader["currentHeaderReq"]);
                        }
                    }
                    db.Close();
                }
             }
                catch (OracleException ex)
                {
                    throw;
                }
            return iDHeaderReq;
        }

        public void CalculateDaysByMonth(List<ChartRequestsViewModel> vacationSubReqs, int year, int[] daysByMonth)
        {
            foreach (var date in vacationSubReqs)
            {
                DateTime firstDayOfMonth = new DateTime(year, date.StartDate.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                DateTime start = date.StartDate;
                if (lastDayOfMonth < date.EndDate)
                {
                    int daysForThisMonth = 0;
                    int daysForNextMonth = 0;
                    while (start <= lastDayOfMonth)
                    {
                        if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                        {
                            ++daysForThisMonth;
                        }
                        start = start.AddDays(1);
                    }
                    daysByMonth[date.StartDate.Month - 1] += daysForThisMonth;
                    daysForNextMonth = date.VacationDays - daysForThisMonth;
                    daysByMonth[date.StartDate.Month] += daysForNextMonth;
                }
                daysByMonth[date.StartDate.Month - 1] += date.VacationDays;
            }
        }
    }
}