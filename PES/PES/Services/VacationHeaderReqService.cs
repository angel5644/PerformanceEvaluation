﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;
using PES.ViewModels;

namespace PES.Services
{
    public class VacationHeaderReqService
    {
        private PESDBContext dbContext = new PESDBContext();
        public VacationHeaderReqService()
        {
            dbContext = new PESDBContext();
        }

        /// <summary>
        /// Metod to get all Vacation Requests by a employee Id 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>A list a Vacation Requests </returns>
        public List<VacHeadReqViewModel> GetGeneralVacationHeaderReqByEmployeeId(int employeeId)
        {
            List<VacHeadReqViewModel> Headers = new List<VacHeadReqViewModel>();
            VacHeadReqViewModel Header = new VacHeadReqViewModel();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string query = @"SELECT " +
                                        "HE.ID_HEADER_REQ, " +
                                        "HE.ID_EMPLOYEE, " +
                                        "HE.TITLE, " +
                                        "HE.NO_VAC_DAYS, " +
                                        "HE.ID_REQ_STATUS," +
                                        "SUB.START_DATE, " +
                                        "SUB.END_DATE, " +
                                        "SUB.RETURN_DATE " +
                                    "FROM " +
                                        "VACATION_HEADER_REQ HE " +
                                    "INNER JOIN VACATION_SUBREQ SUB ON HE.ID_HEADER_REQ = SUB.ID_HEADER_REQ " +
                                    "WHERE " +
                                        "HE.ID_EMPLOYEE = :employeeId " +
                                    "ORDER BY HE.ID_HEADER_REQ";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("employeeId", employeeId));
                        command.ExecuteReader();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Header = new VacHeadReqViewModel();
                            Header.VacationHeaderReqId = Convert.ToInt32(reader["ID_HEADER_REQ"]);
                            Header.EmployeeId = Convert.ToInt32(reader["ID_EMPLOYEE"]);
                            Header.Title = Convert.ToString(reader["TITLE"]);
                            Header.NoVacDays = Convert.ToInt32(reader["NO_VAC_DAYS"]);
                            Header.ReqStatusId = Convert.ToInt32(reader["ID_REQ_STATUS"]);
                            Header.StartDate = Convert.ToDateTime(reader["START_DATE"]);
                            Header.EndDate = Convert.ToDateTime(reader["END_DATE"]);
                            Header.ReturnDate = Convert.ToDateTime(reader["RETURN_DATE"]);
                            Headers.Add(Header);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return Headers;
        }

        /// <summary>
        /// Metod to get all Vacation Request data by a Vacation Resquest Id  
        /// </summary>
        /// <param name="headerId"></param>
        /// <returns>A vacation resquest object </returns>
        public VacHeadReqViewModel GetAllVacRequestInfoByVacReqId(int headerId)
        {
            VacHeadReqViewModel header = new VacHeadReqViewModel();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string query = @"SELECT HE.ID_HEADER_REQ" +
                                           ",HE.ID_EMPLOYEE" +
                                           ",HE.TITLE" + 
                                           ",HE.NO_VAC_DAYS" +
                                           ",HE.COMMENTS" +
                                           ",HE.ID_REQ_STATUS" +
                                           ",HE.REPLAY_COMMENT" +
                                           ",SUB.LEAD_NAME" +
                                           ",SUB.HAVE_PROJECT" +
                                           ",HE.NO_UNPAID_DAYS" +
                                           ",ST.REQ_STATUS" +
                                           ",SUB.START_DATE" +
                                           ",SUB.END_DATE" +
                                           ",SUB.RETURN_DATE " +
                                    " FROM  VACATION_HEADER_REQ HE" +
                                          ",VACATION_SUBREQ SUB " + 
                                          ", VACATION_REQ_STATUS ST" +
                                    " WHERE HE.ID_HEADER_REQ = SUB.ID_HEADER_REQ " +
                                      " AND HE.ID_HEADER_REQ = :headerId" +
                                      " AND HE.ID_REQ_STATUS = ST.ID_REQ_STATUS" +
                                    " ORDER BY HE.ID_HEADER_REQ";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("headerId", headerId));
                        command.ExecuteReader();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            header.VacationHeaderReqId = Convert.ToInt32(reader["ID_HEADER_REQ"]);
                            header.EmployeeId = Convert.ToInt32(reader["ID_EMPLOYEE"]);                            
                            header.Title = Convert.ToString(reader["TITLE"]);
                            header.NoVacDays = Convert.ToInt32(reader["NO_VAC_DAYS"]);
                            header.Comments = Convert.ToString(reader["COMMENTS"]);
                            header.ReqStatusId = Convert.ToInt32(reader["ID_REQ_STATUS"]);
                            header.ReplayComment = Convert.ToString(reader["REPLAY_COMMENT"]);
                            header.LeadName = Convert.ToString(reader["LEAD_NAME"]);
                            header.HaveProject = Convert.ToString(reader["HAVE_PROJECT"]);
                            header.NoUnpaidDays = Convert.ToInt32(reader["NO_UNPAID_DAYS"]);
                            header.status = Convert.ToString(reader["REQ_STATUS"]);
                            header.StartDate = Convert.ToDateTime(reader["START_DATE"]);
                            header.EndDate = Convert.ToDateTime(reader["END_DATE"]);
                            header.ReturnDate = Convert.ToDateTime(reader["RETURN_DATE"]);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return header;
        }

        /// <summary>
        /// Metod to get all Vacation Requests by a employee(Manager) Id 
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns>A list of Vacation Resquests</returns>
        public List<VacHeadReqViewModel> GetAllGeneralVacationHeaderReqByManagerId(int managerId)
        {
            List<VacHeadReqViewModel> Headers = new List<VacHeadReqViewModel>();
            VacHeadReqViewModel Header = new VacHeadReqViewModel();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string query = @"SELECT " +
                                        "EMP.FIRST_NAME, " +
                                        "EMP.LAST_NAME, " +
                                        "HE.ID_HEADER_REQ, " +
                                        "HE.ID_EMPLOYEE, " +
                                        "HE.TITLE, " +
                                        "HE.NO_VAC_DAYS, " +
                                        "HE.ID_REQ_STATUS," +
                                        "SUB.HAVE_PROJECT, " +
                                        "SUB.START_DATE, " +
                                        "SUB.END_DATE, " +
                                        "SUB.RETURN_DATE " +
                                    "FROM " +
                                        "VACATION_HEADER_REQ HE " +
                                    "INNER JOIN EMPLOYEE EMP ON EMP.ID_EMPLOYEE = HE.ID_EMPLOYEE " +
                                    "INNER JOIN VACATION_SUBREQ SUB ON HE.ID_HEADER_REQ = SUB.ID_HEADER_REQ " +
                                    "WHERE " +
                                        "EMP.ID_MANAGER = :managerId " +
                                    "ORDER BY HE.ID_HEADER_REQ";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("managerId", managerId));
                        command.ExecuteReader();
                        OracleDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Header = new VacHeadReqViewModel();
                            Header.FirstName = Convert.ToString(reader["FIRST_NAME"]);
                            Header.LastName = Convert.ToString(reader["LAST_NAME"]);
                            Header.VacationHeaderReqId = Convert.ToInt32(reader["ID_HEADER_REQ"]);
                            Header.EmployeeId = Convert.ToInt32(reader["ID_EMPLOYEE"]);
                            Header.Title = Convert.ToString(reader["TITLE"]);
                            Header.NoVacDays = Convert.ToInt32(reader["NO_VAC_DAYS"]);
                            Header.ReqStatusId = Convert.ToInt32(reader["ID_REQ_STATUS"]);
                            Header.HaveProject = Convert.ToString(reader["HAVE_PROJECT"]);
                            Header.StartDate = Convert.ToDateTime(reader["START_DATE"]);
                            Header.EndDate = Convert.ToDateTime(reader["END_DATE"]);
                            Header.ReturnDate = Convert.ToDateTime(reader["RETURN_DATE"]);
                            Headers.Add(Header);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return Headers;
        }

        /// <summary>
        /// Metod to insert the data of the Object vacHeaderReq in the DB
        /// </summary>
        /// <param name="vacHeadReq"></param>
        /// <returns>True if the insert was successful</returns>
        public bool InsertVacHeaderReq(SendRequestViewModel InsertNewRequest)
        {
            bool status = false;
            int RequestStatus = 1 ;
            int NoUnpaidDays = 1;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string query = "INSERT INTO PE.VACATION_HEADER_REQ" +
                               " (ID_EMPLOYEE," +
                               "TITLE," +
                               "NO_VAC_DAYS," +
                               "COMMENTS," +
                               "ID_REQ_STATUS," +
                               "NO_UNPAID_DAYS)" +
                               "VALUES  (:IdEmployee, :Title, :NoVacDays, :Comments, :IdReqStatus, :NoUnpaidDays)";


                using (OracleCommand command = new OracleCommand(query, db))
                {
                    command.Parameters.Add(new OracleParameter("IdEmployee", InsertNewRequest.EmployeedID));


                    if (InsertNewRequest.TypeRequest == 1)
                    {
                        command.Parameters.Add(new OracleParameter("Title","UNPAID: " + InsertNewRequest.Title));

                    }
                    else if(InsertNewRequest.TypeRequest == 2)
                    {
                        command.Parameters.Add(new OracleParameter("Title","EMERGENCY: " + InsertNewRequest.Title));
                    }
                    else if (InsertNewRequest.TypeRequest == 3)
                    {
                        command.Parameters.Add(new OracleParameter("Title", "FUNERAL: " + InsertNewRequest.Title));
                    }
                    else if (InsertNewRequest.TypeRequest == 4)
                    {
                        command.Parameters.Add(new OracleParameter("Title", "PATERNITY: " + InsertNewRequest.Title));
                    }
                    else
                    {
                        command.Parameters.Add(new OracleParameter("Title", InsertNewRequest.Title));
                    }

                    command.Parameters.Add(new OracleParameter("NoVacDays", InsertNewRequest.daysReq));
                    command.Parameters.Add(new OracleParameter("Comments", InsertNewRequest.Comments));
                    command.Parameters.Add(new OracleParameter("IdReqStatus", RequestStatus));
                    command.Parameters.Add(new OracleParameter("NoUnpaidDays", NoUnpaidDays));

                    try
                    {
                        command.Connection.Open();
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
    }
}