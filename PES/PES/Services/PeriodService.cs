using Oracle.ManagedDataAccess.Client;
using PES.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PES.Models;

namespace PES.Services
{
    public class PeriodService
    {
        private PESDBContext dbContext;

        public PeriodService()
        {
            dbContext = new PESDBContext();
        }

        //Get all periods
        public List<Period> GetAll()
        {
            List<Period> periods = new List<Period>();
            Period employee = new Period();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();
                    string Query = "SELECT ID_PERIOD, " +
                                           "NAME " +
                                           "FROM PERIOD";

                    OracleCommand Comand = new OracleCommand(Query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {

                        // Store data in period object 
                        Period period = new Period();
                        period.PeriodId = Convert.ToInt32(Read["ID_PERIOD"]);
                        period.Name = Convert.ToString(Read["NAME"]);

                        periods.Add(period);
                    }
                    db.Close();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return periods;
        }

        // Get a period by id
        public Period GetPeriodById(int id)
        {
            Period period = new Period();
            try
            {
                using (OracleConnection db = dbContext.GetDBConnection())
                {
                    db.Open();

                    string query = "SELECT ID_PERIOD," +
                                    "NAME " +
                                    "FROM PERIOD WHERE ID_PERIOD = '" + id + "'";

                    OracleCommand Comand = new OracleCommand(query, db);
                    OracleDataReader Read = Comand.ExecuteReader();
                    while (Read.Read())
                    {
                        // Store data in a period object 
                        period = new Period();
                        period.PeriodId = Convert.ToInt32(Read["ID_PERIOD"]);
                        period.Name = Convert.ToString(Read["NAME"]);
                    }
                    db.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return period;
        }
    }
}