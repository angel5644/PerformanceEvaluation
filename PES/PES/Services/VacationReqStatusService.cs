using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class VacationReqStatusService
    {
        private PESDBContext dbContext = new PESDBContext();
        public VacationReqStatusService()
        {
            dbContext = new PESDBContext();
        }

        /// <summary>
        /// Metod to GET the status by Description
        /// </summary>
        /// <param name="description"></param>
        /// <returns>An object VacationReqStatus</returns>
        public VacationReqStatus GetVacationReqStatusByDescription(string description)
        {
            VacationReqStatus status =null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string query = "SELECT " +
                                        "ID_REQ_STATUS, " +
                                            "REQ_STATUS " +
                                    "FROM " +
                                        "VACATION_REQ_STATUS " +
                                    "WHERE " +
                                        "LOWER(REQ_STATUS) = '" + description.ToLower() + "'";
                    OracleCommand command = new OracleCommand();
                    OracleDataReader Read = command.ExecuteReader();

                    while (Read.Read())
                    {
                        status = new VacationReqStatus();
                        status.ReqStatusId = Convert.ToInt32(Read["ID_REQ_STATUS"]);
                        status.Name = Convert.ToString(Read["REQ_STATUS"]);
                    }

                    db.Close();
                }
            }
            catch (Exception xe)
            {
                throw;
            }
            return status;
        }

        /// <summary>
        /// Metod to GET the status by status Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An object of VacationReqStatus</returns>
        public VacationReqStatus GetVacationReqStatusById(int id)
        {
            VacationReqStatus status = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string query = "SELECT " +
                                        "ID_REQ_STATUS, " +
                                        "REQ_STATUS " +
                                    "FROM " +
                                        "VACATION_REQ_STATUS " +
                                    "WHERE " +
                                        "ID_REQ_STATUS = :id";
                    using (OracleCommand command = new OracleCommand(query, db))
                    {
                        command.Parameters.Add(new OracleParameter("id", id));
                        command.ExecuteReader();
                        OracleDataReader Read = command.ExecuteReader();
                        while (Read.Read())
                        {
                            status = new VacationReqStatus();
                            status.ReqStatusId = Convert.ToInt32(Read["ID_REQ_STATUS"]);
                            status.Name = Convert.ToString(Read["REQ_STATUS"]);
                        }
                    }                        
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return status;
        }

       
    }
}