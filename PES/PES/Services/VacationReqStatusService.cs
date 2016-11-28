﻿using System;
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

        public VacationReqStatus GetVacationReqStatusByDescription(string description)
        {
            VacationReqStatus status =null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT " +
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
                        status.reqStatusId = Convert.ToInt32(Read["ID_REQ_STATUS"]);
                        status.name = Convert.ToString(Read["REQ_STATUS"]);
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
    }
}