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
    public class ResendRequestService 
    {
        private PESDBContext dbContext = new PESDBContext();
        public ResendRequestService()
        {
            dbContext = new PESDBContext();
        }

        public ResendRequestViewModel GetRequestInformation(int headerReq)
        {
            ResendRequestViewModel resendRequest = new ResendRequestViewModel(); 

            try
            {

                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string selectAllHolidays = @"Select TITLE , ID_REQ_STATUS, NO_VAC_DAYS ,  REPLAY_COMMENT, START_DATE , END_DATE , RETURN_DATE, HAVE_PROJECT , LEAD_NAME
                                                FROM PE.VACATION_HEADER_REQ  INNER JOIN PE.VACATION_SUBREQ  ON PE.VACATION_HEADER_REQ.ID_HEADER_REQ = PE.VACATION_SUBREQ.ID_HEADER_REQ
                                                WHERE PE.VACATION_HEADER_REQ.ID_HEADER_REQ = :HeaderReqId";

                    using (OracleCommand command = new OracleCommand(selectAllHolidays, db))
                    {
                        command.Parameters.Add(new OracleParameter("HeaderReqId", headerReq));
                        OracleDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            resendRequest.RequestIdResend   = headerReq;
                            resendRequest.TitleResend       = Convert.ToString(reader["TITLE"]);
                            resendRequest.StatusResend      = Convert.ToString(reader["ID_REQ_STATUS"]);
                            resendRequest.NovacDaysResend   = Convert.ToInt32(reader["NO_VAC_DAYS"]);
                            resendRequest.StartDateResend   = Convert.ToDateTime(reader["START_DATE"]);
                            resendRequest.EndDateResend     = Convert.ToDateTime(reader["END_DATE"]);
                            resendRequest.ReturnDateResend  = Convert.ToDateTime(reader["RETURN_DATE"]);
                            resendRequest.LeadNameResend    = Convert.ToString(reader["LEAD_NAME"]);
                            resendRequest.HaveProjectResend = Convert.ToBoolean(reader["HAVE_PROJECT"]);
                            resendRequest.ReplayCommentsResend = Convert.ToString(reader["REPLAY_COMMENT"]);
                        }
                    }
                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }

            return resendRequest;
        }


        /// <summary>
        /// Metod to insert the data of the Object vacHeaderReq in the DB
        /// </summary>
        /// <param name="vacHeadReq"></param>
        /// <returns>True if the insert was successful</returns>
        public bool UpdateResendRequestHeaderReq(ResendRequestViewModel model)
        {
            bool status = false;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string query = @"Update PE.VACATION_HEADER_REQ " +
                               "SET TITLE = :NewTitle, " +
                               "NO_VAC_DAYS = :NewNoVacDays, " +
                               "COMMENTS =  :NewComments , " +
                               "ID_REQ_STATUS =  :NewStatus " +
                               "WHERE ID_HEADER_REQ = :RequestId ";


                using (OracleCommand command = new OracleCommand(query, db))
                {
                    command.Parameters.Add(new OracleParameter("NewTitle", model.TitleResend));
                    command.Parameters.Add(new OracleParameter("NewNoVacDays", model.NovacDaysResend));
                    command.Parameters.Add(new OracleParameter("NewComments", model.CommentsResend));
                    command.Parameters.Add(new OracleParameter("NewStatus", 1));
                    command.Parameters.Add(new OracleParameter("RequestId", model.RequestIdResend));
                   

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

        //Insert Subreq
        public bool UpdateResendRequestSubReq(int idHeaderReq, List<NewVacationDates> model)
        {
            bool status = false;
            using (OracleConnection db = dbContext.GetDBConnection())
            {
                string query = "Update PE.VACATION_SUBREQ " +
                    "SET     START_DATE = :NewSTART_DATE , " +
                            "END_DATE = :NewEND_DATE , " +
                            "RETURN_DATE = :NewRETURN_DATE , " +
                            "HAVE_PROJECT = :NewHAVE_PROJECT , " +
                            "LEAD_NAME = :NewLEAD_NAME " +
                            "WHERE ID_HEADER_REQ = :NewID_HEADER_REQ ";

                using (OracleCommand command = new OracleCommand(query, db))
                {
                    try
                    {
                        command.Connection.Open();
                        foreach (var date in model)
                        {


                            command.Parameters.Add(new OracleParameter("NewSTART_DATE", date.StartDate));
                            command.Parameters.Add(new OracleParameter("NewEND_DATE", date.EndDate));
                            command.Parameters.Add(new OracleParameter("NewRETURN_DATE", Convert.ToDateTime(date.ReturnDate)));
                            command.Parameters.Add(new OracleParameter("NewHAVE_PROJECT", (date.HaveProject).ToString()));
                            command.Parameters.Add(new OracleParameter("NewLEAD_NAME", date.LeadName));
                            command.Parameters.Add(new OracleParameter("NewID_HEADER_REQ", idHeaderReq));
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

    }
}