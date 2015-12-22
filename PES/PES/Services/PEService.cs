﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.DBContext;
using PES.Models;
using Oracle.ManagedDataAccess.Client;

namespace PES.Services
{
    public class PEService
    {
        private PESDBContext dbContext = new PESDBContext();


        public bool InsertPE(PEs pe)
        {
            bool status = false;

            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string InsertQuery = "INSERT INTO PE (EVALUATION_PERIOD," +
                                          "ID_EMPLOYEE," +
                                          "ID_EVALUATOR," +
                                          "ID_STATUS," +
                                          "TOTAL) " +
                                          "VALUES ('" +pe.EvaluationPeriod.ToShortDateString() + "'," +
                                                      pe.EmployeeId + "," +
                                                      pe.EvaluatorId + "," +
                                                      pe.StatusId + "," +
                                                      pe.Total + ")";
                    OracleCommand Command = new OracleCommand(InsertQuery, db);
                    Command.ExecuteNonQuery();
                    status = true;
                    db.Close();
                }
            }
            catch
            {
                status = false;
            }
            
            return status;
        }

        public List<PEs> GetPerformanceEvaluationByUserID(int userid) 
        {
            List<PEs> listPES = null;
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string Query = "SELECT ID_PE," +
                                           "EVALUATION_PERIOD," +
                                           "ID_EMPLOYEE," +
                                           "ID_EVALUATOR," +
                                           "ID_STATUS," +
                                           "TOTAL," +
                                           "ENGLISH_SCORE" +
                                           "FROM EMPLOYEE WHERE ID_EMPLOYEE = '" + userid + "'";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {

                        // Store data in employee object 
                        var pes = new PEs();
                        pes.EmployeeId = Convert.ToInt32(Read["ID_EMPLOYEE"]);
                        pes.PEId = Convert.ToInt32(Read["ID_PE"]);
                        pes.EvaluationPeriod = Convert.ToDateTime(Read["EVALUATION_PERIOD"]);
                        pes.EvaluatorId = Convert.ToInt32(Read["ID_EVALUATOR"]);
                        pes.StatusId = Convert.ToInt32(Read["ID_STATUS"]);
                        pes.Total = Convert.ToDouble(Read["TOTAL"]);
                        pes.EnglishScore = Convert.ToDouble(Read["ENGLISH_SCORE"]);

                        listPES.Add(pes);
                    }
                    
                    db.Close();
                }
            }
            catch
            {
                listPES = null;
            }
            return listPES;
        }

        //public PEs GetPerformanceEvaluationByUser(int userId)
        //{
        //    return null;
        //}
    }
}