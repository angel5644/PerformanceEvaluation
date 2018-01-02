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
    public class VacationSubreqService
    {
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
                            vacationSubReq.HaveProject = Convert.ToChar(Reader["HAVE_PROJECT"]);
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


        public bool InsertSubReq(int idHeaderReq, List<NewVacationDates> model)
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
                            string returnDate = date.ReturnDate;
                            string rMonth = returnDate.Substring(0, 2);
                            string rDay = returnDate.Substring(3, 2);
                            string rYear = returnDate.Substring(6, 4);
                            string FinalReturnDate = (rDay + "/" + rMonth + "/" + rYear);

                            command.Parameters.Add(new OracleParameter("IdHeaderReq", idHeaderReq));
                            command.Parameters.Add(new OracleParameter("StartDate", date.StartDate));
                            command.Parameters.Add(new OracleParameter("EndDate", date.EndDate));
                            command.Parameters.Add(new OracleParameter("ReturnDate", FinalReturnDate));
                            command.Parameters.Add(new OracleParameter("HaveProject", (date.HaveProject).ToString()));
                            command.Parameters.Add(new OracleParameter("LeadName", date.LeadName));                    
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

        public int GetHeaderRequest(InsertNewRequestViewModel data)
        {
            int iDHeaderReq = 0;
            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    
                    string query =  "select   max (id_header_req) as    \"currentHeaderReq\"" + 
                                    "from      PE.VACATION_HEADER_REQ " +
                                    "where     id_employee = '" + data.EmployeeId + "'" +
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