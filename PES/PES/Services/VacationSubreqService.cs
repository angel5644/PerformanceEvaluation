using System;
using System.Collections.Generic;
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
    }
}